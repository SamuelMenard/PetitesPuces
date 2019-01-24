<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="InscriptionProduit.aspx.cs" Inherits="Pages_InscriptionProduit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
   .container-fluid {
      width: 100%;
      max-width: 500px;
   }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBody" Runat="Server">
<div class="container-fluid">
   <h1 class="h3 mb-3 font-weight-normal">Veuillez entrer les informations d'un produit</h1>
   <div class="form-group">
      <asp:DropDownList Id="ddlCategorie" CssClass="form-control" runat="server">
         <asp:ListItem Value="">Sélectionnez la catégorie</asp:ListItem>
      </asp:DropDownList>
   </div>
   <div class="row">
      <div class="form-group col-sm-6">
         <input type="text" class="form-control" id="tbNom" placeholder="Nom" value="" required>
      </div>
      <div class="form-group col-sm-6">
         <div class="input-group">
            <input type="number" step="0.01" class="form-control" id="tbPrixDemande" placeholder="Prix demandé" required>
            <span class="input-group-addon">$</span>
         </div>
      </div>
   </div>
   <div class="form-group">
      <textarea class="form-control" rows="5" id="tbDescription" placeholder="Description" required></textarea>
   </div>
   <div class="form-group">
      <label for="fImage">Sélectionnez une image</label>
      <input type="file" id="fImage" class="form-control" required>
   </div>
   <div class="row">
      <div class="form-group col-sm-6">
         <input type="number" class="form-control" id="tbNbItems" placeholder="Quantité" required>
      </div>
      <div class="form-group col-sm-6">
         <div class="input-group">
            <input type="number" step="0.01" class="form-control" id="tbPrixVente" placeholder="Prix de vente" required>
            <span class="input-group-addon">$</span>
         </div>
      </div>
   </div>
   <div class="form-group">
      <label for="tbDateVente">Sélectionnez une date d’expiration du prix de vente</label>
      <input type="date" class="form-control" id="tbDateVente" required>
   </div>
   <div class="row">
      <div class="form-group col-sm-6">
         <div class="input-group">
            <input type="number" step="0.1" class="form-control" id="tbPoids" placeholder="Poids" required>
            <span class="input-group-addon">lbs</span>
         </div>
      </div>
      <div class="form-group col-sm-6"> 
         <div class="input-group">
    			<label>Disponibilité 
            <div id="radioBtn" class="btn-group">
    				<a class="btn btn-primary active" data-toggle="rbDisponibilite" data-title="O">Oui</a>
    				<a class="btn btn-primary notActive" data-toggle="rbDisponibilite" data-title="N">Non</a>
    		   </div>
    			<input type="hidden" name="rbDisponibilite" id="rbDisponibilite">
            </label>
    		</div>
      </div>
   </div>
   <button class="btn btn-lg btn-primary btn-block" type="submit">Inscrire le produit</button>  
</div>
<script type="text/javascript">
	$('#radioBtn a').on('click', function() {
      var sel = $(this).data('title');
      var tog = $(this).data('toggle');
      $('#'+tog).prop('value', sel);
    
      $('a[data-toggle="'+tog+'"]').not('[data-title="'+sel+'"]').removeClass('active').addClass('notActive');
      $('a[data-toggle="'+tog+'"][data-title="'+sel+'"]').removeClass('notActive').addClass('active');
   })
</script>
</asp:Content>

