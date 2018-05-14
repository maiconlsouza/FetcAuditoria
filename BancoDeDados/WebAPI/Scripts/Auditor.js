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