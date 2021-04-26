<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Escuela.aspx.cs" Inherits="escuela.Escuela" %>

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
                <h5 class="font-weight-bold text-white">Administrador</h5>
            </div>
            <div class="row">
                <div class="col text-white">
                    <div class="dropdown-divider"></div>
                </div>
            </div>
            <div class="menu">
                <a href="Escuela.aspx" class="d-block p-3 text-primary"><i class="fas fa-school mr-2 lead"></i> Escuela</a>
                <a href="MiInfoAdmi.aspx" class="d-block p-3 text-muted"><i class="fas fa-user mr-2 lead"></i> Mi información</a>
                <a href="IndexAdmi.aspx" class="d-block p-3 text-muted"><i class="fas fa-user mr-2 lead"></i> Profesores</a>
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
                        <ul class="navbar-nav ml-auto" id="opNavbar">
                            <li class="nav-item dropdown">
                                <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" runat="server">
                                Alejandro Romero
                                </a>
                                <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                                    <a class="dropdown-item" href="Escuela.aspx"><i class="fas fa-school mr-2 lead"></i> Escuela</a>
                                    <a class="dropdown-item" href="MiInfoAdmi.aspx"><i class="fas fa-user mr-2 lead"></i> Mi información</a>
                                    <a class="dropdown-item" href="IndexAdmi.aspx"><i class="fas fa-user mr-2 lead"></i> Profesores</a>
                                    <a class="dropdown-item" href="EstudianteAdmi.aspx"><i class="fas fa-user mr-2 lead"></i> Estudiantes</a>
                                    <a class="dropdown-item" href="RespaldoAdmi.aspx"><i class="fas fa-database mr-2 lead"></i> Respaldo</a>
                                    <div class="dropdown-divider"></div>
                                    <asp:Button ID="btnSalir" runat="server" Text="Salir" OnClick="btnSalir_Click" CssClass="border-0 bg-white dropdown-item" />                                   
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>

            <div class="container-fluid bg-dark text-white pb-2">
                <div class="row">
                    <div class="col">
                        <h2 class="font-weight-bold">Escuela</h2>
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
                        <div class="row">
                            <div class="col table-wrapper-scroll-y my-custom-scrollbar">

                                <ul class="nav nav-tabs my-3" id="myTab" role="tablist">
                                  <li class="nav-item">
                                    <a class="nav-link active" id="home-tab" data-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true">Grado</a>
                                  </li>
                                  <li class="nav-item">
                                    <a class="nav-link" id="profile-tab" data-toggle="tab" href="#profile" role="tab" aria-controls="profile" aria-selected="false">Materias</a>
                                  </li>
                                </ul>
                                <div class="tab-content" id="myTabContent">
                                  <div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">
                                      <section>
                                          <div class="row my-3">
                                              <div class="col">
                                                  <asp:Label ID="Label1" runat="server" Text="Lista de Grados" CssClass=""></asp:Label>
                                              </div>
                                              <div class="col text-right">
                                                <button class="btn btn-primary" runat="server" onserverclick="btnPDFGrado_Click"><i class="fas fa-print"></i> Imprimir</button>
                                              </div>
                                          </div>
                                          <div class="row">
                                              <div class="col">
                                                  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" Width="100%" AllowSorting="True" CssClass="py-3">
                                                      <AlternatingRowStyle BackColor="White" />
                                                      <Columns>
                                                          <asp:BoundField DataField="nombre" HeaderText="nombre" SortExpression="nombre" />
                                                          <asp:BoundField DataField="seccion" HeaderText="seccion" SortExpression="seccion" />
                                                      </Columns>
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
                                                  <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [nombre], [seccion] FROM [Grado]"></asp:SqlDataSource>
                                              </div>
                                          </div>
                                          
                                      </section>
                                  </div>
                                  <div class="tab-pane fade" id="profile" role="tabpanel" aria-labelledby="profile-tab">
                                      <section>
                                          <div class="row my-3">
                                              <div class="col">
                                                  <asp:Label ID="Label2" runat="server" Text="Lista de Materias" CssClass=""></asp:Label>
                                              </div>
                                              <div class="col text-right">
                                                <button class="btn btn-primary" runat="server" onserverclick="btnPDFMaterias_Click"><i class="fas fa-print"></i> Imprimir</button>
                                              </div>
                                          </div>
                                          <div class="row">
                                              <div class="col">
                                                  <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource2" ForeColor="#333333" GridLines="None" Width="100%" AllowSorting="True" CssClass="py-3">
                                                      <Columns>
                                                          <asp:BoundField DataField="materia" HeaderText="materia" SortExpression="materia" />
                                                      </Columns>
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
                                                  <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [materia] FROM [Materia]"></asp:SqlDataSource>
                                              </div>
                                          </div>
                                      </section>
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
