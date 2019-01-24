<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="SaisieModificationProfil.aspx.cs" Inherits="Pages_SaisieModificationProfil" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
     <link rel="stylesheet" href="../static/style/saisieprofil.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
      <!-- Contenu de la page -->
    <div class="container-fluid">
		<div class="row">
			<div class="well center-block">
				<div class="well-header">
					<h3 class="text-center" style="color:orange"> Profil personnel </h3>
					<hr>
				</div>

				<div class="row">
					<div class="col-md-12 col-sm-12 col-xs-12">
						<div class="form-group">
							<div class="input-group">
								<div class="input-group-addon">
									<i class="glyphicon glyphicon-user"></i>
								</div>
								<input type = "text" placeholder="Prénom" name="txtfname" class="form-control">
								
							</div>
						</div>
					</div>
				</div>

				<div class="row">
					<div class="col-xs-12 col-sm-12 col-md-12">
						<div class="form-group">
							<div class="input-group">
								<div class="input-group-addon">
									<i class="glyphicon glyphicon-user"></i>
								</div>
								<input type = "text" placeholder="Nom" name="txtlname" class="form-control">
							</div>
						</div>
					</div>
				</div>

			    <div class="row">
					<div class="col-md-12 col-xs-12 col-sm-12">
						<div class="form-group">
							<div class="input-group">
								<div class="input-group-addon">
									<i class="glyphicon glyphicon-pencil"></i>
								</div>
								<input type = "text" class="form-control" name="txtmail" placeholder="No civique et rue">
							</div>
						</div>
					</div>
				</div>
                
			    <div class="row">
					<div class="col-md-12 col-xs-12 col-sm-12">
						<div class="form-group">
							<div class="input-group">
								<div class="input-group-addon">
									<i class="glyphicon glyphicon-pencil"></i>
								</div>
								<input type = "text" class="form-control" name="txtmail" placeholder="Ville">
							</div>
						</div>
					</div>
				</div>

                

                    <div class="row bot20">					
						<div class="form-group">
                            <div class="col-md-8 col-xs-8 col-sm-8 ">
                            <asp:Label For="province" runat="server" ID="lblprovince" Text="Province" Font-Size="Medium"></asp:Label>     
                      
                             <asp:DropDownList id="province" CssClass="form-control"  runat="server">
                              <asp:ListItem Selected="True" Value="QC"> Québec </asp:ListItem>
                              <asp:ListItem Value="ON"> Ontario </asp:ListItem>
                              <asp:ListItem Value="NB"> Nouveau-Brunswick </asp:ListItem>
                        </asp:DropDownList>
						</div>
					</div>
                        </div>


                    <div class="row">
					<div class="col-md-12 col-xs-12 col-sm-12">
						<div class="form-group">
							<div class="input-group">
								<div class="input-group-addon">
									<i class="glyphicon glyphicon-pencil"></i>
								</div>
								<input type="text" class="form-control" name="txtCodePostal" placeholder="Code Postal (A9A 9A9)">
							</div>
						</div>
					</div>
				</div>
			                
                    <div class="row">
					<div class="col-md-12 col-xs-12 col-sm-12">
						<div class="form-group">
							<div class="input-group">
								<div class="input-group-addon">
									<i class="glyphicon glyphicon-pencil"></i>
								</div>
								<input type="text" class="form-control" name="txtPays" placeholder="Pays">
							</div>
						</div>
					</div>
				</div>

				<div class="row">
					<div class="col-md-12 col-xs-12 col-sm-12">
						<div class="form-group">
							<div class="input-group">
								<div class="input-group-addon">
									<i class="glyphicon glyphicon-phone-alt"></i>
								</div>
								<input type = "number" minlength="10" maxlength="12" class="form-control" name="txtmobile" placeholder="Telephone">
							</div>
						</div>
					</div>
				</div>

                <div class="row">
					<div class="col-md-12 col-xs-12 col-sm-12">
						<div class="form-group">
							<div class="input-group">
								<div class="input-group-addon">
									<i class="glyphicon glyphicon-phone"></i>
								</div>
								<input type = "number" minlength="10" maxlength="12" class="form-control" name="txtmobile" placeholder="Cellulaire">
							</div>
						</div>
					</div>
				</div>
				

				<div class="row widget">
					<div class="col-md-12 col-xs-12 col-sm-12">
						<button class="btn btn-warning btn-block"> Modifier</button>
					</div>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
