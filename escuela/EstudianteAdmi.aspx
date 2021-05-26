<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EstudianteAdmi.aspx.cs" Inherits="escuela.EstudianteAdmi" %>

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
    <style type="text/css">
        .auto-style1 {
            position: relative;
            width: 100%;
            -ms-flex-preferred-size: 0;
            flex-basis: 0;
            -ms-flex-positive: 1;
            flex-grow: 1;
            max-width: 100%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }
    </style>
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
                <a href="Escuela.aspx" class="d-block p-3 text-muted"><i class="fas fa-school mr-2 lead"></i> Escuela</a>
                <a href="MiInfoAdmi.aspx" class="d-block p-3 text-muted"><i class="fas fa-user mr-2 lead"></i> Mi información</a>
                <a href="IndexAdmi.aspx" class="d-block p-3 text-muted"><i class="fas fa-user mr-2 lead"></i> Profesores</a>
                <a href="CalificarAdmi.aspx" class="d-block p-3 text-muted"><i class="fas fa-database mr-2 lead"></i> Calificar</a>
                <a href="EstudianteAdmi.aspx" class="d-block p-3 text-primary"><i class="fas fa-user mr-2 lead""></i> Estudiantes</a>
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
                                    <asp:Button ID="btnSalir" runat="server" Text="Salir" CssClass="border-0 bg-white dropdown-item" OnClick="btnSalir_Click"/>                                   
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>

            <div class="container-fluid bg-dark text-white pb-2">
                <div class="row">
                    <div class="auto-style1">
                        <h2 class="font-weight-bold">Estudiantes</h2>
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

                                <section>
                                    <div class="container-fluid shadow my-4 px-4 py-4">
                                        <div class="row my-3">
                                            <div class="col">
                                                <asp:Label ID="Label1" runat="server" Text="Lista de estudiantes"></asp:Label>
                                            </div>
                                            <div class="col">
                                                <button class="btn btn-primary" runat="server" onserverclick="btnPDFAlumnos_Click"><i class="fas fa-print"></i> Imprimir</button>
                                            </div>
                                        </div>

                                        <div class="row my-2">
                                            <div class="col">
                                                <asp:Label ID="Label2" runat="server" Text="Nombre:"></asp:Label>
                                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col">
                                                <asp:Label ID="Label21" runat="server" Text="Apellido:"></asp:Label>
                                                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row my-2">
                                            <div class="col">
                                                <asp:Label ID="lb2" runat="server" Text="Grados:"></asp:Label>
                                                <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlDataSource1" DataTextField="Grado" DataValueField="idGrado" CssClass="form-control"></asp:DropDownList>
                                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="select (Grado.nombre+' '+Grado.seccion) as Grado,Grado.idGrado from Grado inner join Profesores on Profesores.idGrado = Grado.idGrado where Profesores.nivel &lt;&gt; 1 ;"></asp:SqlDataSource>
                                            </div>
                                        </div>
                                        <div class="row my-2">
                                            <div class="col">
                                                <asp:Label ID="lb3" runat="server" Text="Nombre del responsable:"></asp:Label>
                                                <asp:TextBox ID="txtresponsableNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col my-2">
                                                <asp:Label ID="lb4" runat="server" Text="Apellido del responsable:"></asp:Label>
                                                <asp:TextBox ID="txtresponsableApellido" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row my-2">
                                            <div class="col">
                                                <asp:Label ID="lb5" runat="server" Text="Email:"></asp:Label>
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col">
                                                <asp:Label ID="lb7" runat="server" Text="Contraseña"></asp:Label>
                                                <asp:TextBox ID="txtContra" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row my-2">
                                            <div class="col">

                                                <button class="btn btn-success" runat="server" onserverclick="GuardarEstudiante"><i class="fas fa-plus"></i> Registar estudiante</button>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col">

                                                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" CellPadding="4" DataKeyNames="idAlumno" DataSourceID="SqlDataSource3" ForeColor="#333333" GridLines="None" Width="100%">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:BoundField DataField="idAlumno" HeaderText="idAlumno" InsertVisible="False" ReadOnly="True" SortExpression="idAlumno" />
                                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" 
                                                            SortExpression="nombre" />
                                                        <asp:BoundField DataField="apellido" HeaderText="Apellido" 
                                                            SortExpression="apellido" />
                                                        <asp:BoundField DataField="carnet" HeaderText="Carnet" 
                                                            SortExpression="carnet" />
                                                        <asp:TemplateField HeaderText="Grado" SortExpression="idGrado">
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="DropDownList4" runat="server" 
                                                                    DataSourceID="SqlDataSource1" DataTextField="Grado" 
                                                                    DataValueField="idGrado" Enabled="False">
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbGrado" runat="server" Text='<%# Bind("idGrado") %>' 
                                                                    Visible="False"></asp:Label>
                                                                <asp:DropDownList ID="DropDownList3" runat="server" 
                                                                    DataSourceID="SqlDataSource14" DataTextField="Grado" DataValueField="Grado">
                                                                </asp:DropDownList>
                                                                <asp:SqlDataSource ID="SqlDataSource14" runat="server" 
                                                                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                                                                    SelectCommand="select (nombre+' '+seccion) as 'Grado' from Grado where idGrado = @idGrado">
                                                                    <SelectParameters>
                                                                        <asp:ControlParameter ControlID="lbGrado" Name="idGrado" PropertyName="Text" />
                                                                    </SelectParameters>
                                                                </asp:SqlDataSource>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="responsableNombre" HeaderText="Nombre responsable" 
                                                            SortExpression="responsableNombre" />
                                                        <asp:BoundField DataField="responsableApellido" 
                                                            HeaderText="Apellido  responsable" SortExpression="responsableApellido" />
                                                        <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                                                        <asp:BoundField DataField="usuario" HeaderText="Usuario" 
                                                            SortExpression="usuario" />
                                                        <asp:BoundField DataField="contra" HeaderText="Contrasena" 
                                                            SortExpression="contra" />
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
                                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" DeleteCommand="DELETE FROM [Alumnos] WHERE [idAlumno] = @idAlumno" InsertCommand="INSERT INTO [Alumnos] ([nombre], [apellido], [carnet], [idGrado], [responsableNombre], [responsableApellido], [email], [usuario], [contra]) VALUES (@nombre, @apellido, @carnet, @idGrado, @responsableNombre, @responsableApellido, @email, @usuario, @contra)" SelectCommand="SELECT * FROM [Alumnos]" UpdateCommand="UPDATE [Alumnos] SET [nombre] = @nombre, [apellido] = @apellido, [carnet] = @carnet, [idGrado] = @idGrado, [responsableNombre] = @responsableNombre, [responsableApellido] = @responsableApellido, [email] = @email, [usuario] = @usuario, [contra] = @contra WHERE [idAlumno] = @idAlumno">
                                                    <DeleteParameters>
                                                        <asp:Parameter Name="idAlumno" Type="Int64" />
                                                    </DeleteParameters>
                                                    <InsertParameters>
                                                        <asp:Parameter Name="nombre" Type="String" />
                                                        <asp:Parameter Name="apellido" Type="String" />
                                                        <asp:Parameter Name="carnet" Type="String" />
                                                        <asp:Parameter Name="idGrado" Type="Int32" />
                                                        <asp:Parameter Name="responsableNombre" Type="String" />
                                                        <asp:Parameter Name="responsableApellido" Type="String" />
                                                        <asp:Parameter Name="email" Type="String" />
                                                        <asp:Parameter Name="usuario" Type="String" />
                                                        <asp:Parameter Name="contra" Type="String" />
                                                    </InsertParameters>
                                                    <UpdateParameters>
                                                        <asp:Parameter Name="nombre" Type="String" />
                                                        <asp:Parameter Name="apellido" Type="String" />
                                                        <asp:Parameter Name="carnet" Type="String" />
                                                        <asp:Parameter Name="idGrado" Type="Int32" />
                                                        <asp:Parameter Name="responsableNombre" Type="String" />
                                                        <asp:Parameter Name="responsableApellido" Type="String" />
                                                        <asp:Parameter Name="email" Type="String" />
                                                        <asp:Parameter Name="usuario" Type="String" />
                                                        <asp:Parameter Name="contra" Type="String" />
                                                        <asp:Parameter Name="idAlumno" Type="Int64" />
                                                    </UpdateParameters>
                                                </asp:SqlDataSource>
                                            </div>
                                        </div>
                                    </div>
                                </section>

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
