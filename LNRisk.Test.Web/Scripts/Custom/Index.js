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

        switch (recipient) {
            case "New":
                _this.blockFields(false);
                _this.showModal(recipient);
                break;
            case "Edit":
                _this.blockFields(false);
                _this.fillFields(btn);
                _this.showModal(recipient);
                break;
            case "Delete":
                _this.fillFields(btn);
                _this.blockFields(true);
                _this.showModal(recipient);
                break;
            case "Search":
                _this.showResults(btn);
                break;
        }
    };

    this.showResults = function(btn) {
        var data = btn.closest('tr');

        var model = {};

        model.Id = data.find('.row_id').html();;
        model.Payload = data.find('.row_value').html();;

        $.post('/Home/Result',
            model,
            function(data) {
                if (data.result) {
                    $('#resultDateAmount').html(data.DateAmount);
                    $('#resultLetters').html(data.Letters);
                    $('#resultPalindrome').html(data.Palindrome);
                    $("#myData").modal('toggle');
                } else {
                    alert(data.message);
                }
            });
    };

    this.addContent = function () {
        var model = {};

        model.Id = $('#txtId').val();
        model.Payload = $('#txtPayload').val();

        $.post('/Home/Save',
            model,
            function (data) {
                if (data.result) {
                    _this.hideModal()
                    _this.updateTable();
                }

                alert(data.message);
            });
    };

    this.editContent = function() {
        var model = {};

        model.Id = $('#txtId').val();
        model.Payload = $('#txtPayload').val();

        $.post('/Home/Edit',
            model,
            function (data) {
                _this.hideModal()
                alert(data.message);
                _this.updateTable();
            });
    };

    this.removeContent = function () {
        var model = {};

        model.Id = $('#txtId').val();
        model.Payload = $('#txtPayload').val();

        $.post('/Home/Remove',
            model,
            function (data) {
                _this.hideModal()
                alert(data.message);
                _this.updateTable();
            });
    };

    this.updateTable = function () {

        $('#myTable').DataTable().destroy();

        var buttonEdit =
            "<a href='#' class='btn-edit col-md-3' data-action='Edit'><i class='fa fa-pencil-square-o fa-2x' data-action='Edit' aria-hidden='true'></i></a>";

        var buttonDelete =
            "<a href='#' data-action='Delete' class='col-md-3'><i class='fa fa-times-circle fa-2x btn-remove' data-action='Delete' aria-hidden='true'></i></a>";

        var buttonDetails =
            "<a href='#' data-action='Search' class='col-md-3'><i class='fa fa-search fa-2x btn-search' data-action='Search' aria-hidden='true'></i></a>";

        $('#myTable').DataTable({
            ajax: '/Home/List',
            "columnDefs": [
                { "width": "5%", "class" : "row_id","targets": 0 },
                { "width": "90%", "class" : "row_value","targets": 1 },
                {
                    "width": "5%",
                    "targets": 2,
                    "data": null,
                    "defaultContent": buttonDetails +  buttonEdit + buttonDelete
                }
            ],
            columns: [{ data: 'Id' }, { data: 'Payload' }],
            initComplete: function () {
                $('.btn-edit').on('click', _this.formContent);
                $('.btn-remove').on('click', _this.formContent);
                $('.btn-search').on('click', _this.formContent);
            }
        });
    };

    this.showButton = function (recipient) {
        $('.btn-action').hide();
        $('#btn' + recipient).show();
    }

    this.blockFields = function (block) {
        $('.form-control').attr('disabled', block);
    };


    this.showModal = function (recipient) {
        var modal = $('#myModal');
        modal.modal('toggle');
        modal.find('.modal-title').text(recipient + ' Item');
        _this.showButton(recipient);
    };

    this.hideModal = function (recipient) {
        var modal = $('#myModal');
        modal.modal('toggle');
    };

    this.fillFields = function(btn) {
        var data = btn.closest('tr');

        var id = data.find('.row_id').html();
        var value = data.find('.row_value').html();

        $('#txtId').val(id);
        $('#txtPayload').val(value);

        $('#txtId').attr('disabled', true);
    };
}