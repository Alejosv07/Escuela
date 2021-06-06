<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MiInfoAdmi.aspx.cs" Inherits="escuela.MiInfoAdmi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="assets/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/fonts/fontawesome-all.min.css" />
    <link rel="stylesheet" href="assets/css/dash.css" />
    <title>Inicio</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="d-flex my-4 mx-4">
        <div class="sidebar-container">
            <div class="logo">
                <img src="assets/img/Logo.png" class="my-3"/>
                <h5 class="font-weight-bold text-white">Administrador</h5>
            </div>
            <div class="row">
                <div class="col text-white">
                    <div class="dropdown-divider"></div>
                </div>
            </div>
            <div class="menu">
                <a href="Escuela.aspx" class="d-block p-3 text-muted"><i class="fas fa-school mr-2 lead"></i> Escuela</a>
                <a href="MiInfoAdmi.aspx" class="d-block p-3 text-primary"><i class="fas fa-user mr-2 lead"></i> Mi información</a>
                <a href="IndexAdmi.aspx" class="d-block p-3 text-muted"><i class="fas fa-user mr-2 lead"></i> Profesores</a>
                <a href="CalificarAdmi.aspx" class="d-block p-3 text-muted"><i class="fas fa-book-open mr-2 lead"></i> Calificar</a>
                <a href="EstudianteAdmi.aspx" class="d-block p-3 text-muted"><i class="fas fa-user mr-2 lead"></i> Estudiantes</a>
                <a href="RespaldoAdmi.aspx" class="d-block p-3 text-muted"><i class="fas fa-database mr-2 lead"></i> Respaldo</a>
            </div>
        </div>

        <!--Contenido-->

        <div class="w-100 contenido">
            <nav class="navbar navbar-expand-lg navbar-dark bg-dark headerContenido">
                <div class="container">
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="collapse navbar-collapse" id="navbarSupportedContent">
                        <ul class="navbar-nav ml-auto">
                            <li class="nav-item dropdown font-weight-bold">
                                <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" runat="server">
                                Alejandro Romero
                                </a>
                                <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                                    <a class="dropdown-item" href="Escuela.aspx"><i class="fas fa-school mr-2 lead"></i> Escuela</a>
                                    <a class="dropdown-item" href="MiInfoAdmi.aspx"><i class="fas fa-user mr-2 lead"></i> Mi información</a>
                                    <a class="dropdown-item" href="IndexAdmi.aspx"><i class="fas fa-user mr-2 lead"></i> Profesores</a>
                                    <a class="dropdown-item" href="CalificarAdmi.aspx"><i class="fas fa-book-open mr-2 lead"></i> Calificar</a><a class="dropdown-item" href="EstudianteAdmi.aspx"><i class="fas fa-user mr-2 lead"></i> Estudiantes</a>
                                    <a class="dropdown-item" href="EstudianteAdmi.aspx"><i class="fas fa-user mr-2 lead"></i> Estudiantes</a>
                                    <a class="dropdown-item" href="RespaldoAdmi.aspx"><i class="fas fa-database mr-2 lead"></i> Respaldo</a>
                                    <div class="dropdown-divider"></div>
                                    <asp:Button ID="btnSalir" runat="server" Text="Salir" CssClass="border-0 bg-white dropdown-item" OnClick="btnSalir_Click"/>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>

            <div class="container-fluid bg-dark text-white pb-2 headerActualizacion">
                <div class="row">
                    <div class="col">
                        <h2 class="font-weight-bold">Mi información</h2>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <asp:Label ID="lbActu" class="font-weight-bold" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="contenidoB">
                <section>
                    <div class="container my-5">
                        <div class="row align-content-center justify-content-center vh-100 text-center">
                            <div class="col-sm-8 col-md-6">
                                <div class="frm-container">
                                    <div class="input-group my-2">
                                        <label for="txtNombre" class="mr-2">Nombres:</label>
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" name="txtCarnet" placeholder="2018LR601" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="input-group my-4">
                                        <label for="txtApellidos" class="mr-2">Apellidos:</label>
                                        <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" name="txtCarnet" placeholder="2018LR601" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="input-group my-4">
                                        <label for="txtGrado" class="mr-2">Grado:</label>
                                        <asp:TextBox ID="txtGrado" runat="server" CssClass="form-control" name="txtCarnet" placeholder="2018LR601" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="input-group my-4">
                                        <label for="txtEmail" class="mr-2">E-mail:</label>
                                        <asp:TextBox ID="txtEmail" TextMode="Email" runat="server" CssClass="form-control" name="txtCarnet" placeholder="2018LR601"></asp:TextBox>
                                    </div>
                                    <div class="input-group my-4">
                                        <label for="Usuario" class="mr-2">Usuario:</label>
                                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" name="txtCarnet" placeholder="2018LR601" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="input-group my-4">
                                        <label for="txtPass1" class="mr-2">Contraseña:</label>
                                        <asp:TextBox ID="txtPass1" runat="server" CssClass="form-control" name="txtCarnet" placeholder="2018LR601"></asp:TextBox>
                                    </div>
                                    <div class="input-group my-4">
                                        <label for="txtPass2" class="mr-2">Repetir contraseña:</label>
                                        <asp:TextBox ID="txtPass2" runat="server" CssClass="form-control" name="txtCarnet" placeholder="2018LR601"></asp:TextBox>
                                    </div>
                                    <button id="btnGuardarInfo" class="btn btn-success" runat="server" onserverclick="btnGuardarInfo_Click"><i class="fas fa-save"></i> Guardar información</button>
                                    <button id="btnImprimir" class="btn btn-primary" runat="server" onserverclick="btnImprimir_Click"><i class="fas fa-print"></i> Imprimir</button>

                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
        </div>
        </div>
        </div>
    </form>

    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
</body>
</html>
