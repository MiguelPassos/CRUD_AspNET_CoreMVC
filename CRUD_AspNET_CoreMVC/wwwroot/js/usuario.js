$('#divCreate #btnSalvarUsuario').on('click', function () {
    var nome = $('#divCreate .txtNome').val();
    var dataNascimento = $('#divCreate .txtDataNascimento').val();

    var usuario = { "Codigo": 0, "Nome": nome, "DataNascimento": new Date(Date.parse(dataNascimento, "yyyy-MM-dd")) };

    $.ajax({
        url: 'Usuario/CreateUsuario',
        //dataType: "json",
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(usuario),
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (response) {
            window.location.reload();
        },
        error: function () {
            bootbox.alert('Falha na inclusão do usuário.');
        }
    });
});

$('#divEdit #btnSalvarUsuario').on('click', function () {
    var codigo = $('#divEdit .txtCodigo').val();
    var nome = $('#divEdit .txtNome').val();
    var dataNascimento = $('#divEdit .txtDataNascimento').val();

    var usuario = { "Codigo": codigo, "Nome": nome, "DataNascimento": new Date(Date.parse(dataNascimento, "yyyy-MM-dd")) };

    $.ajax({
        url: 'Usuario/UpdateUsuario',
        //dataType: "json",
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(usuario),
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (response) {            
            window.location.reload();
        },
        error: function () {
            bootbox.alert('Falha na inclusão do usuário.');
        }
    });
});

$(':button.btnDetail').on('click', function () {
    var codigo = $(this).attr('name');
    
    $.ajax({
        url: 'Usuario/GetDetails?codigo=' + codigo,        
        contentType: 'application/json; charset=utf-8',
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (response) {
            if (response != 'Não encontrado.' && response != 'Inválido.') {
                $('#divDetail .txtCodigo').val(response.codigo);
                $('#divDetail .txtNome').val(response.nome);
                $('#divDetail .txtDataNascimento').val(response.dataNascimento.substring(0, 10));
                $('#divDetail').modal('show');
            }            
        },
        error: function () {
            bootbox.alert('Falha ao obter dados do usuário.');
        }
    });
});

$(':button.btnEdit').on('click', function () {
    var codigo = $(this).attr('name');

    $.ajax({
        url: 'Usuario/GetDetails?codigo=' + codigo,
        contentType: 'application/json; charset=utf-8',
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (response) {
            if (response != 'Não encontrado.' && response != 'Inválido.') {
                $('#divEdit .txtCodigo').val(response.codigo);
                $('#divEdit .txtNome').val(response.nome);
                $('#divEdit .txtDataNascimento').val(response.dataNascimento.substring(0, 10));
                $('#divEdit').modal('show');
            }
        },
        error: function () {
            bootbox.alert('Falha ao obter dados do usuário.');
        }
    });
});

$(':button.btnDelete').on('click', function () {
    var codigo = $(this).attr('name');

    event.preventDefault();
    bootbox.confirm('Deseja realmente excluir o Usuário?', function (result) {
        if (result) {
            $.ajax({
                url: 'Usuario/DeleteUsuario?codigo=' + codigo,
                contentType: 'application/json; charset=utf-8',
                headers: {
                    RequestVerificationToken:
                        $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                success: function (response) {
                    window.location.reload();
                },
                error: function () {
                    bootbox.alert('Falha ao obter dados do usuário.');
                }
            });
        }
    });    
});