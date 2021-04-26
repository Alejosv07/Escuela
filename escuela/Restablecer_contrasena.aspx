<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Restablecer_contrasena.aspx.cs" Inherits="escuela.Restablecer_contrasena" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" href="assets/bootstrap/css/bootstrap.min.css"/>
    <link rel="stylesheet" href="assets/fonts/fontawesome-all.min.css"/>
    <link rel="stylesheet" href="assets/css/styles.css"/>
    <title>Restablecer BALR</title>
</head>
<body>
    <form id="form1" runat="server">
        

        <div class="container-fluid ">
        <div class="row align-content-center justify-content-center vh-100 text-center text-white">
            <div class="col-sm-8 col-md-6 col-lg-3">
                <div class="frm-container">
                    <img src="assets/img/Logo.png" class="img-thumbnail my-4"/>
                    <h3 class="font-weight-bold">Nueva contraseña</h3>
                    <small class="text-muted font-weight-bold">Olvide contraseña>Verificación de código>Restablecer contraseña</small>
                    <div class="input-group my-4">
                        <asp:TextBox id="txtPassNew" type="text" CssClass="form-control" placeholder="Password" runat="server"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnPassNew" Text="Restablecer contraseña" runat="server" CssClass="btn btn-primary btn-block my-4" OnClick="btnPassNew_Click" />
                    <a href="Login.aspx" class="text-muted">Identificarme</a>
                </div>

            </div>
        </div>
        </div>


    </form>
</body>
</html>
