function ModuleIndex() {
    var _this = this;

    this.startModule = function () {
        $('#btnAddModal').on("click", _this.formContent);
        $('#btnNew').on("click", _this.addContent);
        $('#btnEdit').on("click", _this.editContent);
        $('#btnDelete').on("click", _this.removeContent);
        this.updateTable();
    };

    this.formContent = function (event) {
        var btn = $(event.target);
        var recipient = btn.data('action');
        _this.showModal(recipient);
    };

    this.addContent = function () {
        var model = {};

        model.Id = $('#txtId').val();
        model.Payload = $('#txtPayload').val();

        $.post('/Home/Save',
            model,
            function (data) {
                _this.hideModal()
                alert(data.message);
                _this.updateTable();
            });
    };

    this.editContent = function () {

    }

    this.removeContent = function () {
        alert();
    };

    this.updateTable = function () {

        $('#myTable').DataTable().destroy();

        $('#myTable').DataTable({
            ajax: '/Home/List',
            "columnDefs": [
                { "width": "5%", "targets": 0 },
                { "width": "90%", "targets": 1 },
                {
                    "width": "5%",
                    "targets": 2,
                    "data": null,
                    "defaultContent": "<a href='#' class='btn-edit' data-action='Edit'><i class='fa fa-pencil-square-o fa-2x' data-action='Edit' aria-hidden='true'></i></a> " +
                        "&nbsp; <a href='#' data-action='Delete'><i class='fa fa-times-circle fa-2x btn-remove' data-action='Delete' aria-hidden='true'></i></a>"
                }
            ],
            columns: [{ data: 'Id' }, { data: 'Payload' }],
            initComplete: function () {
                $('.btn-edit').on('click', _this.formContent);
                $('.btn-remove').on('click', _this.formContent);
            }
        });
    };

    this.showButton = function (recipient) {
        $('.btn-action').hide();
        $('#btn' + recipient).show();
    }


    this.showModal = function(recipient) {
        var modal = $('#myModal');
        modal.modal('toggle');
        modal.find('.modal-title').text(recipient + ' Item');
        _this.showButton(recipient);
    };

    this.hideModal = function (recipient) {
        var modal = $('#myModal');
        modal.modal('toggle');
    }
}