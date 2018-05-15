Login = function (Email, Senha, ReturnUrl) {
    $.ajax({
        type: 'post',
        url: '/Login/DoLogin',
        data: { usuario: Email, senha: Senha },
        beforeSend: function () {
            MSConfirmaForm($("#btn-login"), false);
        },
        complete: function () {
            MSConfirmaForm($("#btn-login"), true);
        },
        success: function (data) {
            console.log(data);
            if (data.situacao === false) {
                MSAlerta("warning", "Atenção!", data.mensagem, $("#ms-alerta"));
            } else {
                if (ReturnUrl === "")
                    window.location = "/";
                else
                    window.location = ReturnUrl;
            }
        }
    });
};

MSConfirmaForm = function (componente, habilita) {
    if (habilita) {
        componente.attr("disabled", false);
        componente.css("cursor", "pointer");
    } else {
        componente.closest("body").find("#ms-alerta").empty();
        componente.attr("disabled", true);
        componente.css("cursor", "wait");
    }
};

MSAlerta = function (tipo, titulo, texto, area) {
    alert(texto);
};

ComboGrupos = function () {
    var combo = $("#grupo");

    $.ajax({
        type: 'post',
        url: '/api/grupo/obtem',
        data: $("#form").serialize(),
        success: function (data) {
            if (data.Sucesso == true) {
                combo.empty();

                $.map(data.Objeto, function (item) {
                    combo.append(
                        "<option value='" + item.Id + "'>" + item.Nome + "</option>"
                    );
                });
            }
        }
    });
};

SalvarArquivo = function () {
    $.ajax({
        type: 'post',
        url: '/Envio/DoSave',
        data: { descricao: $("#arquivo").val(), grupo: $("#grupo").val() },
        beforeSend: function () {
            MSConfirmaForm($("#Salvar"), false);
        },
        complete: function () {
            MSConfirmaForm($("#Salvar"), true);
        },
        success: function (data) {
            if (data.situacao === false) {
                MSAlerta("warning", "Atenção!", data.mensagem, $("#ms-alerta"));
            } else {
                window.location = "/Envio/Upar/" + data.Objeto.Id;                
            }
        }
    });
};

function EnviaArquivo(obj) {
    $("#cadproduto").modal('show');

    $('#subir').fileupload({
        dataType: 'json',
        url: '/Util/UploadFiles',
        autoUpload: true,
        done: function (e, data) {
            $("#cadproduto").modal('hide');
            $('.progress .progress-bar').css('width', 0 + '%');

            location.reload();
        }
    }).on('fileuploadprogressall', function (e, data) {
        var progress = parseInt(data.loaded / data.total * 100, 10);
        $('.progress .progress-bar').css('width', progress + '%');
    });
}

CarregaMeuDash = function () {
    $.ajax({
        type: 'post',
        url: '/Home/MeuDashboard',
        success: function (data) {
            $("#nao_lidos").html("<h3>" + data.NaoLidos + " não visualizados</h3>");
            $("#lidos").html("<h3>" + data.Lidos + " visualizados</h3>");
            $("#meus_arquivos").html("<h3>" + (data.NaoLidos + data.Lidos) + " disponíveis</h3>");
        }
    });
};

MarcarLido = function (id) {
    $.ajax({
        type: 'post',
        url: '/Arquivos/MarcarComoLido',
        data: {id: id},
        success: function (data) {
            location.reload();
        }
    });
};