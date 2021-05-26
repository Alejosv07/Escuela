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
                <a href="IndexProfesores.aspx" class="d-block p-3 text-primary"><i class="fas fa-book-open mr-2 lead"></i> Calificar</a>
                <a href="ReportesMaestros.aspx" class="d-block p-3 text-muted"><i class="fas fa-book-open mr-2 lead"></i> Reportes</a>            
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
                                <div class="row pt-4 px-2">
                                    <div class="col">
                                        <span class="border bg-light">
                                            <asp:Label ID="lbGrado" runat="server" Text="Grado" CssClass="font-weight-bold py-4 px-4"></asp:Label>
                                        </span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col my-4">
                                        <div class="bg-success rounded py-1" style="width: 75%">
                                            <asp:Label Text="Materia: " runat="server" CssClass="text-white px-2"/>
                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="btn btn-success dropdown-toggle" 
                                                DataSourceID="SqlDataSource2" DataTextField="Materia" 
                                                DataValueField="idMateria" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                                                SelectCommand="select Materia.materia as 'Materia',Materia.idMateria from Evaluaciones inner join Materia on Materia.idMateria = Evaluaciones.idMateria where Evaluaciones.idProfesores = @idProfesores group by Materia.materia,Materia.idMateria;">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="txtProfesorSeleccionado" Name="idProfesores" 
                                                        PropertyName="Text" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </div>
                                    </div>
                                    <div class="col my-4">
                                        <div class="bg-success rounded py-1" style="width: 45%">
                                            <asp:Label Text="Trimestre: " runat="server" CssClass="text-white px-2"/>
                                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="btn btn-success dropdown-toggle" 
                                                 DataSourceID="SqlDataSource3" DataTextField="Trimestre" 
                                                 DataValueField="Trimestre" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                                                SelectCommand="select Evaluaciones.idTrimestre as 'Trimestre' from Evaluaciones inner join Materia on Materia.idMateria = Evaluaciones.idMateria where Evaluaciones.idProfesores = @idProfesores group by Evaluaciones.idTrimestre;">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="txtProfesorSeleccionado" Name="idProfesores" 
                                                        PropertyName="Text" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </div>
                                    </div>
                                </div>

                                <asp:TextBox ID="txtProfesorSeleccionado" runat="server" CssClass="form-control d-none"></asp:TextBox>

                                <asp:GridView ID="GridView1" runat="server" AutoGenerateEditButton="True" AutoGenerateColumns="False" DataKeyNames="idEvaluaciones" DataSourceID="SqlDataSource11" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" OnRowDataBound="GridView1_RowDataBound">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="idEvaluaciones" HeaderText="idEvaluaciones" InsertVisible="False" ReadOnly="True" SortExpression="idEvaluaciones" />
                                        <asp:BoundField DataField="Apellido" HeaderText="Apellido" SortExpression="Apellido" ReadOnly="True"/>
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" ReadOnly="True"/>
                                        <asp:BoundField DataField="Evaluacion1" HeaderText="Evaluacion1" SortExpression="Evaluacion1" />
                                        <asp:BoundField DataField="Evaluacion2" HeaderText="Evaluacion2" SortExpression="Evaluacion2" />
                                        <asp:BoundField DataField="Evaluacion3" HeaderText="Evaluacion3" SortExpression="Evaluacion3" />
                                        <asp:BoundField DataField="Evaluacion4" HeaderText="Evaluacion4" SortExpression="Evaluacion4" />
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

                                <asp:SqlDataSource ID="SqlDataSource11" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Evaluaciones.idEvaluaciones,Alumnos.apellido as Apellido,Alumnos.nombre as 'Nombre', Evaluaciones.evaluacion1 as 'Evaluacion1', Evaluaciones.evaluacion2 as 'Evaluacion2', Evaluaciones.evaluacion3 as 'Evaluacion3',Evaluaciones.evaluacion4 as 'Evaluacion4' FROM Evaluaciones INNER JOIN Alumnos ON Evaluaciones.idAlumno = Alumnos.idAlumno WHERE (Evaluaciones.idMateria = @idMateria and Evaluaciones.idProfesores = @idProfesores and Evaluaciones.idTrimestre = @idTrimestre)" UpdateCommand="UPDATE Evaluaciones SET evaluacion1 = @evaluacion1, evaluacion2 = @evaluacion2, evaluacion3 = @evaluacion3,evaluacion4 = @evaluacion4 WHERE (idEvaluaciones = @idEvaluaciones)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="DropDownList1" Name="idMateria" PropertyName="SelectedValue" />
                                        <asp:ControlParameter ControlID="txtProfesorSeleccionado" Name="idProfesores" PropertyName="Text" />
                                        <asp:ControlParameter ControlID="DropDownList2" Name="idTrimestre" PropertyName="SelectedValue" />
                                    </SelectParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="evaluacion1" />
                                        <asp:Parameter Name="evaluacion2" />
                                        <asp:Parameter Name="evaluacion3" />
                                        <asp:Parameter Name="evaluacion4" />
                                        <asp:Parameter Name="idEvaluaciones" />
                                    </UpdateParameters>
                                </asp:SqlDataSource>

                                

                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Evaluaciones.idEvaluaciones,Alumnos.apellido as Apellido,Alumnos.nombre as 'Nombre', Evaluaciones.evaluacion1 as 'Evaluacion1', Evaluaciones.evaluacion2 as 'Evaluacion2', Evaluaciones.evaluacion3 as 'Evaluacion3' FROM Evaluaciones INNER JOIN Alumnos ON Evaluaciones.idAlumno = Alumnos.idAlumno WHERE (Evaluaciones.idMateria = @idMateria and Evaluaciones.idProfesores = @idProfesores and Evaluaciones.idTrimestre = @idTrimestre)" UpdateCommand="UPDATE Evaluaciones SET evaluacion1 = @evaluacion1, evaluacion2 = @evaluacion2, evaluacion3 = @evaluacion3 WHERE (idEvaluaciones = @idEvaluaciones)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="DropDownList1" Name="idMateria" PropertyName="SelectedValue" />
                                        <asp:ControlParameter ControlID="txtProfesorSeleccionado" Name="idProfesores" PropertyName="Text" />
                                        <asp:ControlParameter ControlID="DropDownList2" Name="idTrimestre" PropertyName="SelectedValue" />
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
                            <div class="col">
                                <button class="btn btn-primary" runat="server" onserverclick="btnTF_Click"><i class="fas fa-print"></i> Imprimir promedios finales</button>
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
