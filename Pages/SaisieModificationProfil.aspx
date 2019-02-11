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
                                <asp:TextBox ID="tbPrenom" placeholder="Prénom" CssClass="form-control" runat="server"></asp:TextBox> 
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
                                <asp:TextBox ID="tbNom" placeholder="Nom" CssClass="form-control" runat="server"></asp:TextBox>
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
                                <asp:TextBox ID="tbRue" placeholder="No civique et rue" CssClass="form-control" runat="server"></asp:TextBox>
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
                                <asp:TextBox ID="tbVille" placeholder="Ville" CssClass="form-control" runat="server"></asp:TextBox>
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
                                <asp:TextBox ID="tbCodePostal" placeholder="Code Postal (A9A 9A9)" CssClass="form-control" runat="server"></asp:TextBox>
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
                                <asp:TextBox ID="tbCode" placeholder="Code Postal (A9A 9A9)" CssClass="form-control" runat="server"></asp:TextBox>
								<input type="text" class="form-control" name="txtPays" placeholder="Pays">
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
								<input type = "number" minlength="10" maxlength="12" class="form-control" name="txtmobile" placeholder="Telephone">
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
								<input type = "number" minlength="10" maxlength="12" class="form-control" name="txtmobile" placeholder="Cellulaire">
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
