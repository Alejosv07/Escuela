<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexProfesores.aspx.cs" Inherits="escuela.IndexProfesores" %>

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
                <h5 class="font-weight-bold text-white">Profesor</h5>
            </div>
            <div class="row">
                <div class="col text-white">
                    <div class="dropdown-divider"></div>
                </div>
            </div>
            <div class="menu">
                <a href="MiInfoProfesor.aspx" class="d-block p-3 text-muted"><i class="fas fa-user mr-2 lead"></i> Mi información</a>
                <a href="IndexProfesores.aspx" class="d-block p-3 text-primary"><i class="fas fa-book-open lead"></i> Calificar</a>
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
                        <h2 class="font-weight-bold">Calificar</h2>
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
                                <div class="row">
                                    <div class="col">
                                        <div class="dropdown my-4">
                                          <button class="btn btn-success dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            Materias
                                          </button>
                                          <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                              <asp:Button ID="btnLenguaje" runat="server" Text="Lenguaje" CssClass="border-0 bg-white dropdown-item" OnClick="btnP1_Click"/>                                   
                                                <div class="dropdown-divider"></div>
                                              <asp:Button ID="btnSociales" runat="server" Text="Sociales" CssClass="border-0 bg-white dropdown-item" OnClick="btnP2_Click"/>                                   
                                                <div class="dropdown-divider"></div>
                                            <asp:Button ID="btnMatematicas" runat="server" Text="Matematicás" CssClass="border-0 bg-white dropdown-item" OnClick="btnP3_Click"/>                                   
                                                <div class="dropdown-divider"></div>
                                              <asp:Button ID="btnCiencia" runat="server" Text="Ciencias" CssClass="border-0 bg-white dropdown-item" OnClick="btnP4_Click"/>                                   
                                                <div class="dropdown-divider"></div>
                                              <asp:Button ID="btnIngles" runat="server" Text="Ingles" CssClass="border-0 bg-white dropdown-item" OnClick="btnP5_Click"/>                                   
                                                <div class="dropdown-divider"></div>
                                          </div>
                                        </div>
                                        <asp:TextBox ID="txtMateriaShow" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col">
                                        <div class="dropdown my-4">
                                          <button class="btn btn-success dropdown-toggle" type="button" id="dropdownMenuButton2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            Trimestres
                                          </button>
                                          <div class="dropdown-menu" aria-labelledby="dropdownMenuButton2">
                                              <asp:Button ID="Button1" runat="server" Text="Primer Trimestre" CssClass="border-0 bg-white dropdown-item" OnClick="btnT1_Click"/>                                   
                                                <div class="dropdown-divider"></div>
                                              <asp:Button ID="Button2" runat="server" Text="Segundo Trimestre" CssClass="border-0 bg-white dropdown-item" OnClick="btnT2_Click"/>                                   
                                                <div class="dropdown-divider"></div>
                                            <asp:Button ID="Button3" runat="server" Text="Tercer Trimestre" CssClass="border-0 bg-white dropdown-item" OnClick="btnT3_Click"/>                                   
                                                <div class="dropdown-divider"></div>
                                          </div>
                                        </div>
                                        <asp:TextBox ID="txtTrimestreShow" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                <asp:TextBox ID="txtMateriaSeleccionada" runat="server" CssClass="form-control d-none"></asp:TextBox>
                                <asp:TextBox ID="txtProfesorSeleccionado" runat="server" CssClass="form-control d-none"></asp:TextBox>
                                <asp:TextBox ID="txtTrimestreSeleccionado" runat="server" CssClass="form-control d-none"></asp:TextBox>


                                <asp:GridView ID="GridView1" runat="server" AutoGenerateEditButton="True" AutoGenerateColumns="False" DataKeyNames="idEvaluaciones" DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="idEvaluaciones" HeaderText="idEvaluaciones" InsertVisible="False" ReadOnly="True" SortExpression="idEvaluaciones" />
                                        <asp:BoundField DataField="apellido" HeaderText="apellido" SortExpression="apellido" />
                                        <asp:BoundField DataField="nombre" HeaderText="nombre" SortExpression="nombre" />
                                        <asp:BoundField DataField="evaluacion1" HeaderText="evaluacion1" SortExpression="evaluacion1" />
                                        <asp:BoundField DataField="evaluacion2" HeaderText="evaluacion2" SortExpression="evaluacion2" />
                                        <asp:BoundField DataField="evaluacion3" HeaderText="evaluacion3" SortExpression="evaluacion3" />
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

                                

                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Evaluaciones.idEvaluaciones, Alumnos.apellido, Alumnos.nombre, Evaluaciones.evaluacion1, Evaluaciones.evaluacion2, Evaluaciones.evaluacion3 FROM Evaluaciones INNER JOIN Alumnos ON Evaluaciones.idAlumno = Alumnos.idAlumno WHERE (Evaluaciones.idMateria = @idMateria and Evaluaciones.idProfesores = @idProfesores and Evaluaciones.idTrimestre = @idTrimestre)" UpdateCommand="UPDATE Evaluaciones SET evaluacion1 = @evaluacion1, evaluacion2 = @evaluacion2, evaluacion3 = @evaluacion3 WHERE (idEvaluaciones = @idEvaluaciones)" OnSelecting="SqlDataSource1_Selecting">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtMateriaSeleccionada" Name="idMateria" PropertyName="Text" />
                                        <asp:ControlParameter ControlID="txtProfesorSeleccionado" Name="idProfesores" PropertyName="Text" />
                                        <asp:ControlParameter ControlID="txtTrimestreSeleccionado" Name="idTrimestre" PropertyName="Text" />
                                    </SelectParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="evaluacion1" />
                                        <asp:Parameter Name="evaluacion2" />
                                        <asp:Parameter Name="evaluacion3" />
                                        <asp:Parameter Name="idEvaluaciones" />
                                    </UpdateParameters>
                                </asp:SqlDataSource>

                                

                            </div>
                        </div>
                        <div class="row my-2">
                            <div class="col">
                                <button class="btn btn-primary" runat="server" onserverclick="btnImprimirClick"><i class="fas fa-print"></i> Imprimir</button>
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
