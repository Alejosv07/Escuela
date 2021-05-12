<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportesMaestros.aspx.cs" Inherits="escuela.ReportesMaestros" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" href="assets/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/fonts/fontawesome-all.min.css" />
    <link rel="stylesheet" href="assets/css/dash.css" />
    <title></title>
</head>
<body>
        <form id="form1" runat="server">
        
        <div class="d-flex my-4 mx-4">
        <div class="sidebar-container">
            <div class="logo">
                <img src="assets/img/Logo.png" class="my-3"/>
                <h5 class="font-weight-bold text-white">Profesor</h5>
            </div>
            <div class="row">
                <div class="col text-white">
                    <div class="dropdown-divider"></div>
                </div>
            </div>
            <div class="menu">
                <a href="MiInfoProfesor.aspx" class="d-block p-3 text-muted"><i class="fas fa-user mr-2 lead"></i> Mi información</a>
                <a href="IndexProfesores.aspx" class="d-block p-3 text-muted"><i class="fas fa-book-open mr-2 lead"></i> Calificar</a>
                <a href="ReportesMaestros.aspx" class="d-block p-3 text-primary"><i class="fas fa-book-open mr-2 lead"></i> Reportes</a>
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
                        <ul class="navbar-nav ml-auto" id="opNavbar">
                            <li class="nav-item dropdown">
                                <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" runat="server">
                                Alejandro Romero
                                </a>
                                <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                                    <a class="dropdown-item" href="MiInfoProfesor.aspx"><i class="fas fa-calendar-alt lead"></i> Mi información</a>
                                    <a class="dropdown-item" href="IndexProfesores.aspx"><i class="fas fa-book-open lead"></i> Calificaciones</a>
                                    <a class="dropdown-item" href="ReportesMaestros.aspx"><i class="fas fa-book-open lead"></i> Reportes</a>
                                    <div class="dropdown-divider"></div>
                                    <asp:Button ID="btnSalir" runat="server" Text="Salir" CssClass="border-0 bg-white dropdown-item" OnClick="btnSalir_Click"/>                                   
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>

            <div class="container-fluid bg-dark text-white pb-2">
                <div class="row">
                    <div class="col">
                        <h2 class="font-weight-bold">Reportes</h2>
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
                    <div class="container py-4">
                        <div class="row">
                            <div class="col table-wrapper-scroll-y my-custom-scrollbar">
                                <div class="row">
                                    <div class="col">
                                        <button id="btnImprimirNomina" class="btn btn-primary" runat="server" onserverclick="btnImprimirNominaClick"><i class="fas fa-print"></i> Nomina de alumnos</button>
                                    </div>
                                    <div class="col">
                                        <button id="btnImprimirPromediosFinales" class="btn btn-primary" runat="server" onserverclick="btnImprimirPromedioClick"><i class="fas fa-print"></i> Promedios alumnos</button>
                                    </div>
                                    <div class="col">
                                        <button id="btnImprimirAsistencia" class="btn btn-primary" runat="server" onserverclick="btnImprimirClick"><i class="fas fa-print"></i> Asistencia</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>


        </div>
    </div>

    </form>

    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
</body>
</html>
