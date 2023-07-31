$(function () {
    var l = abp.localization.getResource('SingerApp');
    var getFilter = function () {
        return {
            filter: $("input[name='Search'").val(),
            countryId: $("[name='CountryId'").val()
        };
    };

    var dataTable = $('#SingersTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[0, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(singerApp.singers.singer.getList, getFilter),
            columnDefs: [
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('CountryName'),
                    data: "countryName"
                },
                {
                    title: l('IsActive'),
                    data: "isActive",
                    render: function (data) {
                        console.log(data);
                        return data ?
                            `<span class="badge bg-success">${l("Active")}</span>`
                            :
                            `<span class="badge bg-danger">${l("InActive")}</span>`

                    }
                },
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l('SingerDeletionConfirmationMessage',
                                            data.record.name);
                                    },
                                    action: function (data) {
                                        singerApp.singers.singer
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                }

            ]
        })
    );
    $("input[name='Search'").blur(function () {
        dataTable.ajax.reload();
    });
    $("[name='CountryId'").change(function () {
        dataTable.ajax.reload();
    });

    var createModal = new abp.ModalManager(abp.appPath + 'Singers/CreateSingerModal');
    createModal.onResult(function () {
        dataTable.ajax.reload();
    });
    $('#NewSingerButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    var editModal = new abp.ModalManager(abp.appPath + 'Singers/EditSingerModal');
    editModal.onResult(function () {
        dataTable.ajax.reload();
    });
});