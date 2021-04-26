<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexAdmi.aspx.cs" Inherits="escuela.IndexAdmi" %>

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
                <a href="Escuela.aspx" class="d-block p-3 text-muted"><i class="fas fa-school mr-2 lead"></i> Escuela</a>
                <a href="MiInfoAdmi.aspx" class="d-block p-3 text-muted"><i class="fas fa-user mr-2 lead"></i> Mi información</a>
                <a href="IndexAdmi.aspx" class="d-block p-3 text-primary"><i class="fas fa-user mr-2 lead"></i> Profesores</a>
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
                                    <a class="dropdown-item" href="MiInfoAdmi.aspx"><i class="fas fa-user lead"></i> Mi información</a>
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
                    <div class="col">
                        <h2 class="font-weight-bold">Profesores</h2>
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
                            <div class="col table-wrapper-scroll-y my-custom-scrollbar bg-light">

                                <div class="shadow my-2 py-4 px-4">
                                     <div class="row my-2">
                                         <div class="col">
                                             <asp:Label ID="Label8" runat="server" Text="Lista de profesores"></asp:Label>
                                         </div>
                                        <div class="col">
                                            <button class="btn btn-primary" runat="server" onserverclick="btnPDFProfesores_Click"><i class="fas fa-print"></i> Imprimir</button>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col">
                                            <asp:Label ID="Label1" runat="server" Text="Nombre:"></asp:Label>
                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col">
                                            <asp:Label ID="Label2" runat="server" Text="Apellido:"></asp:Label>
                                            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col">
                                            <asp:Label ID="Label3" runat="server" Text="Grado:"></asp:Label>
                                            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="Grado" DataValueField="idGrado" CssClass="form-control"></asp:DropDownList>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="select (nombre+' '+seccion) as Grado,idGrado from Grado"></asp:SqlDataSource>
                                        </div>
                                        <div class="col">
                                            <asp:Label ID="Label4" runat="server" Text="Email:"></asp:Label>
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col">
                                            <asp:Label ID="Label5" runat="server" Text="Usuario:"></asp:Label>
                                            <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col">
                                            <asp:Label ID="Label6" runat="server" Text="Contraseña:"></asp:Label>
                                            <asp:TextBox ID="txtContra" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col">
                                            <asp:Label ID="Label7" runat="server" Text="Nivel:"></asp:Label>
                                            <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0" Selected="True">Profesor</asp:ListItem>
                                                <asp:ListItem Value="1">Administrador</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row my-2">
                                         <div class="col">
                                             <button class="btn btn-success" runat="server" onserverclick="GuardarEstudiante"><i class="fas fa-plus"></i> Registar Profesor</button>
                                         </div>
                                    </div>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" DataKeyNames="idProfesores" DataSourceID="SqlDataSource3" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="idProfesores" HeaderText="idProfesores" InsertVisible="False" ReadOnly="True" SortExpression="idProfesores" />
                                            <asp:BoundField DataField="nombre" HeaderText="nombre" SortExpression="nombre" />
                                            <asp:BoundField DataField="apellido" HeaderText="apellido" SortExpression="apellido" />
                                            <asp:TemplateField HeaderText="idGrado" SortExpression="idGrado">
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource4" DataTextField="Grado" DataValueField="idGrado" SelectedValue='<%# Bind("idGrado") %>'>
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="select (nombre+' '+seccion) as Grado,idGrado from Grado"></asp:SqlDataSource>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("idGrado") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
                                            <asp:BoundField DataField="usuario" HeaderText="usuario" SortExpression="usuario" />
                                            <asp:BoundField DataField="contra" HeaderText="contra" SortExpression="contra" />
                                            <asp:TemplateField HeaderText="nivel" SortExpression="nivel">
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="DropDownList2" runat="server" SelectedValue='<%# Bind("nivel") %>' Width="118px">
                                                        <asp:ListItem Value="0">Profesor</asp:ListItem>
                                                        <asp:ListItem Value="1">Administrador</asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("nivel") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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

                                     <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Profesores]" DeleteCommand="DELETE FROM [Profesores] WHERE [idProfesores] = @idProfesores" InsertCommand="INSERT INTO [Profesores] ([nombre], [apellido], [idGrado], [email], [usuario], [contra], [nivel]) VALUES (@nombre, @apellido, @idGrado, @email, @usuario, @contra, @nivel)" UpdateCommand="UPDATE [Profesores] SET [nombre] = @nombre, [apellido] = @apellido, [idGrado] = @idGrado, [email] = @email, [usuario] = @usuario, [contra] = @contra, [nivel] = @nivel WHERE [idProfesores] = @idProfesores">
                                         <DeleteParameters>
                                             <asp:Parameter Name="idProfesores" Type="Int32" />
                                         </DeleteParameters>
                                         <InsertParameters>
                                             <asp:Parameter Name="nombre" Type="String" />
                                             <asp:Parameter Name="apellido" Type="String" />
                                             <asp:Parameter Name="idGrado" Type="Int32" />
                                             <asp:Parameter Name="email" Type="String" />
                                             <asp:Parameter Name="usuario" Type="String" />
                                             <asp:Parameter Name="contra" Type="String" />
                                             <asp:Parameter Name="nivel" Type="Int32" />
                                         </InsertParameters>
                                         <UpdateParameters>
                                             <asp:Parameter Name="nombre" Type="String" />
                                             <asp:Parameter Name="apellido" Type="String" />
                                             <asp:Parameter Name="idGrado" Type="Int32" />
                                             <asp:Parameter Name="email" Type="String" />
                                             <asp:Parameter Name="usuario" Type="String" />
                                             <asp:Parameter Name="contra" Type="String" />
                                             <asp:Parameter Name="nivel" Type="Int32" />
                                             <asp:Parameter Name="idProfesores" Type="Int32" />
                                         </UpdateParameters>
                                     </asp:SqlDataSource>
                                     <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>

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
