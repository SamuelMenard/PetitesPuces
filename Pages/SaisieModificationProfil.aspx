<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="SaisieModificationProfil.aspx.cs" Inherits="Pages_SaisieModificationProfil" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
     <link rel="stylesheet" href="../static/style/saisieprofil.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
      <!-- Contenu de la page -->
    <asp:Panel runat="server" CssClass="container-fluid modifierContainer">
		<asp:Panel runat="server"  CssClass="row">
			<asp:Panel runat="server"  CssClass="well center-block">
				<asp:Panel runat="server"  CssClass="well-header">
					<h3 class="text-center" style="color:orange"> Profil personnel </h3>
					<hr>
				</asp:Panel>

				<asp:Panel runat="server"  CssClass="row">
					<asp:Panel runat="server"  CssClass="col-md-12 col-sm-12 col-xs-12">
						<asp:Panel runat="server"  CssClass="form-group">
							<asp:Panel runat="server"  CssClass="input-group">
								<asp:Panel runat="server"  CssClass="input-group-addon">
									<i class="glyphicon glyphicon-user"></i>
								</asp:Panel>
                                <asp:TextBox runat="server"  placeholder="Prénom" ID="txtPrenom" CssClass="form-control" ></asp:TextBox>
								
								
							</asp:Panel>
						</asp:Panel>
					</asp:Panel>
				</asp:Panel>

				<asp:Panel runat="server"  CssClass="row">
					<asp:Panel runat="server" CssClass="col-xs-12 col-sm-12 col-md-12">
						<asp:Panel runat="server"  CssClass="form-group">
							<asp:Panel runat="server"  CssClass="input-group">
								<asp:Panel runat="server"  CssClass="input-group-addon">
									<i class="glyphicon glyphicon-user"></i>
								</asp:Panel>
                                <asp:TextBox runat="server"  placeholder="Nom" ID="txtNom" CssClass="form-control" ></asp:TextBox>								
							</asp:Panel>
						</asp:Panel>
					</asp:Panel>
				</asp:Panel>

                   <asp:Panel runat="server" CssClass="row">
					<asp:Panel runat="server" CssClass="col-md-12 col-xs-12 col-sm-12">
						<asp:Panel runat="server" CssClass="form-group">
							<asp:Panel runat="server" CssClass="input-group">
								<asp:Panel runat="server" CssClass="input-group-addon">
									<i class="glyphicon glyphicon-envelope"></i>
								</asp:Panel>
                                <asp:TextBox runat="server" Enabled="false"  placeholder="" ID="txtEmail" CssClass="form-control" ></asp:TextBox>								
							</asp:Panel>
						</asp:Panel>
					</asp:Panel>
				</asp:Panel>

			    <asp:Panel runat="server" CssClass="row">
					<asp:Panel runat="server" CssClass="col-md-12 col-xs-12 col-sm-12">
						<asp:Panel runat="server" CssClass="form-group">
							<asp:Panel runat="server" CssClass="input-group">
								<asp:Panel runat="server" CssClass="input-group-addon">
									<i class="glyphicon glyphicon-pencil"></i>
								</asp:Panel>
                                <asp:TextBox runat="server"  placeholder="No civique et rue" ID="txtRue" CssClass="form-control" ></asp:TextBox>								
							</asp:Panel>
						</asp:Panel>
					</asp:Panel>
				</asp:Panel>
                
			    <asp:Panel runat="server" CssClass="row">
					<asp:Panel runat="server" CssClass="col-md-12 col-xs-12 col-sm-12">
						<asp:Panel runat="server" CssClass="form-group">
							<asp:Panel runat="server" CssClass="input-group">
								<asp:Panel runat="server" CssClass="input-group-addon">
									<i class="glyphicon glyphicon-pencil"></i>
								</asp:Panel>								
                                <asp:TextBox runat="server"  placeholder="Ville" ID="txtVille" CssClass="form-control" ></asp:TextBox>
							</asp:Panel>
						</asp:Panel>
					</asp:Panel>
				</asp:Panel>

                

                    <asp:Panel runat="server" CssClass="row bot20">					
						<asp:Panel runat="server" CssClass="form-group">
                            <asp:Panel runat="server" CssClass="col-md-8 col-xs-8 col-sm-8 ">
                            <asp:Label For="province" runat="server" ID="lblprovince" Text="Province" Font-Size="Medium"></asp:Label>     
                      
                             <asp:DropDownList id="province" CssClass="form-control"  runat="server">
                              <asp:ListItem Selected="True" Value="QC"> Québec </asp:ListItem>
                              <asp:ListItem Value="ON"> Ontario </asp:ListItem>
                              <asp:ListItem Value="NB"> Nouveau-Brunswick </asp:ListItem>
                        </asp:DropDownList>
						</asp:Panel>
					</asp:Panel>
                        </asp:Panel>


                    <asp:Panel runat="server" CssClass="row">
					<asp:Panel runat="server" CssClass="col-md-12 col-xs-12 col-sm-12">
						<asp:Panel runat="server" CssClass="form-group">
							<asp:Panel runat="server" class="input-group">
								<asp:Panel runat="server" CssClass="input-group-addon">
									<i class="glyphicon glyphicon-pencil"></i>
								</asp:Panel>
                                <asp:TextBox runat="server"  placeholder="Code Postal (A9A 9A9)" ID="txtCodePostal" CssClass="form-control" ></asp:TextBox>
							</asp:Panel>
						</asp:Panel>
					</asp:Panel>
				</asp:Panel>
			                
                    <asp:Panel runat="server" CssClass="row">
					<asp:Panel runat="server" CssClass="col-md-12 col-xs-12 col-sm-12">
						<asp:Panel runat="server" CssClass="form-group">
							<asp:Panel runat="server" CssClass="input-group">
								<asp:Panel runat="server" CssClass="input-group-addon">
									<i class="glyphicon glyphicon-pencil"></i>
								</asp:Panel>
                                <asp:TextBox runat="server"  placeholder="Pays" ID="txtPays" CssClass="form-control" ></asp:TextBox>
							</asp:Panel>
						</asp:Panel>
					</asp:Panel>
				</asp:Panel>

				<asp:Panel runat="server" CssClass="row">
					<asp:Panel runat="server" CssClass="col-md-12 col-xs-12 col-sm-12">
						<asp:Panel runat="server" CssClass="form-group">
							<asp:Panel runat="server" CssClass="input-group">
								<asp:Panel runat="server" CssClass="input-group-addon">
									<i class="glyphicon glyphicon-phone-alt"></i>
								</asp:Panel>
                                <asp:TextBox runat="server"  placeholder="Telephone 999 999-999" ID="txtTelephone" MaxLength="12" minlength="10" CssClass="form-control" ></asp:TextBox>
							</asp:Panel>
						</asp:Panel>
					</asp:Panel>
				</asp:Panel>

                <asp:Panel runat="server" CssClass="row">
					<asp:Panel runat="server" CssClass="col-md-12 col-xs-12 col-sm-12">
						<asp:Panel runat="server" CssClass="form-group">
							<asp:Panel runat="server" CssClass="input-group">
								<asp:Panel runat="server" CssClass="input-group-addon">
									<i class="glyphicon glyphicon-phone"></i>
								</asp:Panel>
                               <asp:TextBox runat="server"  placeholder="Cellulaire 999 999-999" ID="txtCellulaire" MaxLength="12" minlength="10" CssClass="form-control" ></asp:TextBox>
							</asp:Panel>
						</asp:Panel>
					</asp:Panel>
				</asp:Panel>
				

				<asp:Panel runat="server" CssClass="row widget">
					<asp:Panel runat="server" CssClass="col-md-12 col-xs-12 col-sm-12">
						<asp:Button ID="btnModifier" CssClass="btn btn-warning btn-block" runat="server" />
					</asp:Panel>
				</asp:Panel>
			</asp:Panel>
		</asp:Panel>
	</asp:Panel>
</asp:Content>
