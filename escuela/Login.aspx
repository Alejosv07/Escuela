<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="escuela.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" href="assets/bootstrap/css/bootstrap.min.css"/>
    <link rel="stylesheet" href="assets/fonts/fontawesome-all.min.css"/>
    <link rel="stylesheet" href="assets/css/styles.css"/>
    <title>Login BALR</title>
</head>
<body>
    <form id="form1" runat="server">
        
        <div class="container-fluid ">
        <div class="row align-content-center justify-content-center vh-100 text-center text-white">
            <div class="col-sm-8 col-md-6 col-lg-3">
                <div class="frm-container">
                    <img src="assets/img/Logo.png" class="img-thumbnail my-4"/>
                    <h3 class="font-weight-bold">Login</h3>
                    <div class="input-group my-4">
                        <asp:TextBox ID="txtEmail" TextMode="Email" CssClass="form-control" name="email" placeholder="Email" runat="server"></asp:TextBox>
                        <button class="btn btn-incluide position-absolute"><i class="fas fa-user"></i></button>
                    </div>
                    <div class="input-group my-4">
                        <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" name="password" placeholder="Password" runat="server"></asp:TextBox>
<%--                        <div class="input-group-append">
                            <button id="show_password" class="btn btn-light"><i class="fas fa-eye-slash"></i></button>
                        </div>--%>
                    </div>
                    <asp:Button ID="btnIdentificarme" CssClass="btn btn-primary btn-block my-4" runat="server" Text="Identificarme" OnClick="btnIdentificarme_Click"/>
                    <a href="Restablecer.aspx" class="text-muted">Restablecer contraseña</a>
                </div>

            </div>
        </div>
    </div>

    </form>
</body>
</html>
