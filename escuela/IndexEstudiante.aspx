<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexEstudiante.aspx.cs" Inherits="escuela.IndexEstudiante" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" href="assets/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/fonts/fontawesome-all.min.css" />
    <link rel="stylesheet" href="assets/css/dash.css" />
    <title>Inicio</title>
</head>
<body>
    <form id="form1" runat="server">

        <div class="d-flex my-4 mx-4">
        <div class="sidebar-container">
            <div class="logo">
                <img src="assets/img/Logo.png" class="my-3"/>
                <h5 class="font-weight-bold text-white">Estudiante</h5>
            </div>
            <div class="row">
                <div class="col text-white">
                    <div class="dropdown-divider"></div>
                </div>
            </div>
            <div class="menu">
                <a href="MiInfoEstudiante.aspx" class="d-block p-3 text-muted"><i class="fas fa-user mr-2 lead"></i> Mi información</a>
                <a href="IndexEstudiante.aspx" class="d-block p-3 text-primary"><i class="fas fa-book-open mr-2 lead"></i> Calificaciones</a>
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
                                    <a class="dropdown-item" href="MiInfoEstudiante.aspx"><i class="fas fa-user mr-2 lead"></i> Mi información</a>
                                    <a class="dropdown-item" href="IndexEstudiante.aspx"><i class="fas fa-book-open mr-2 lead"></i> Calificaciones</a>
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
                        <h2 class="font-weight-bold">Calificaciones</h2>
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
                    <div class="container">
                        <div class="row  pt-4 px-2 ">
                            <div class="col table-wrapper-scroll-y my-custom-scrollbar">
                                <div class="row">
                                    <div class="col">
                                        <span class="border bg-light">
                                            <asp:Label ID="lbGrado" runat="server" Text="Grado" CssClass="font-weight-bold py-4 px-4"></asp:Label>
                                        </span>
                                    </div>
                                </div>
                                <div class="dropdown py-3">
                                    <button class="btn btn-success dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        Periodos
                                    </button>
                                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                        <asp:Button ID="btnPeriodo1" runat="server" Text="Periodo 1" CssClass="border-0 bg-white dropdown-item" OnClick="btnP1_Click"/>                                   
                                        <div class="dropdown-divider"></div>
                                        <asp:Button ID="btnPeriodo2" runat="server" Text="Periodo 2" CssClass="border-0 bg-white dropdown-item" OnClick="btnP2_Click"/>                                   
                                        <div class="dropdown-divider"></div>
                                        <asp:Button ID="btnPeriodo3" runat="server" Text="Periodo 3" CssClass="border-0 bg-white dropdown-item" OnClick="btnP3_Click"/>                                   
                                        <div class="dropdown-divider"></div>
                                        <asp:Button ID="btnFperiodo" runat="server" Text="Promedios Finales" CssClass="border-0 bg-white dropdown-item" OnClick="btnPF_Click"/>                                   
                                    </div>
                                </div>
                                
                                <asp:GridView ID="grid1" runat="server" CssClass="table" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>


                            </div>
                        </div>
                        <div class="row my-2">
                            <div class="col">
                                <button class="btn btn-primary" runat="server" onserverclick="btnPDF_Click"><i class="fas fa-print"></i> Imprimir</button>
                            </div>
                        </div>
                    </div>
                </section>
            </div>

            <!-- Modal -->
            <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header bg-light text-white">
                            <h5 class="modal-title" id="exampleModalLabel">NOTAS PARCIALES PRIMER PERIODO</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="container">
                                <div class="row">
                                    <div class="col">
                                        <table class="table">
                                            <thead>
                                                <tr>
                                                    <th scope="col">Eva. 01</th>
                                                    <th scope="col">Eva. 02</th>
                                                    <th scope="col">Eva. 03</th>
                                                    <th scope="col">Eva. 04</th>
                                                    <th scope="col">Eva. 05</th>
                                                    <th scope="col">Parcial</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>9</td>
                                                    <td>9</td>
                                                    <td>9</td>
                                                    <td>9</td>
                                                    <td>9</td>
                                                    <td>9</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="far fa-times-circle"></i> Cerrar</button>
                            <button type="button" class="btn btn-primary" data-dismiss="modal"><i class="fas fa-print"></i> Imprimir</button>
                        </div>
                    </div>
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
