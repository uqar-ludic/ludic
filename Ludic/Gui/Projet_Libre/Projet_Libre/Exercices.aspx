<%@ Page Title="Exercices" Language="C#" MasterPageFile="~/SandBox.Master" AutoEventWireup="true" CodeBehind="Exercices.aspx.cs" Inherits="Projet_Libre.Exercices" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LeftContent" runat="server">
    <div>
        <div id="actionEdition">
            <ul class="nav nav-list"> 
                <li><a href="#"><i class="glyphicon glyphicon-floppy-disk"></i> Rechercher un exercice</a></li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2> Tableau des exercices</h2>
    <table class="table">
        <thead>
            <tr>
                <th> # </th>
                <th> Thème </th>
                <th> Exercices </th>
                <th> Actions </th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td> <span class="label label-default"> 1 </span> </td>
                <td> <a data-toggle="modal" data-target="#desc">Orienté objet</a> </td>
                <td >
                    <a class="label label-primary" data-toggle="modal" data-target="#exe">1.1</a>
                    <a class="label label-primary" data-toggle="modal" data-target="#exe">1.2</a>
                    <a class="label label-primary" data-toggle="modal" data-target="#exe">1.3</a>
                    <a class="label label-primary" data-toggle="modal" data-target="#exe">1.4</a>
                    <a class="label label-primary" data-toggle="modal" data-target="#exe">1.5</a>
                    <a class="label label-primary" data-toggle="modal" data-target="#exe">1.6</a>
                    <a class="label label-primary" data-toggle="modal" data-target="#exe">1.7</a>
                </td>
                <td>
                    <input type="checkbox"/> activé
                </td>
            </tr>
            <tr>
                <td> <span class="label label-default"> 2 </span> </td>
                <td> <a data-toggle="modal" data-target="#desc">Récursivité</a> </td>
                <td >   
                     <a class="label label-primary" data-toggle="modal" data-target="#exe">2.1</a>
                    <a class="label label-primary" data-toggle="modal" data-target="#exe">2.2</a>
                    <a class="label label-primary" data-toggle="modal" data-target="#exe">2.3</a>
                    <a class="label label-primary" data-toggle="modal" data-target="#exe">2.4</a>
                    <a class="label label-primary" data-toggle="modal" data-target="#exe">2.5</a>
                    <a class="label label-primary" data-toggle="modal" data-target="#exe">2.6</a>
                    <a class="label label-primary" data-toggle="modal" data-target="#exe">2.7</a>
                </td>
                 <td>
                    <input type="checkbox"/> activé
                </td>
            </tr>
        </tbody>
    </table>
    <div class="modal fade" id="exe" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
        <h4 class="modal-title" id="myModalLabel">Exercice n° 1.1</h4>
      </div>
      <div class="modal-body">
          <ul class="nav nav-tabs">
          <li><a href="#home" data-toggle="tab">Description</a></li>
          <li><a href="#profile" data-toggle="tab">Succès</a></li>
        </ul>
        <div class="tab-content">
          <div class="tab-pane active" id="home"><h4>Groupe d'exercice : <small>orienté objet</small></h4>
        <h4>Description : </h4>
          <p>
              Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
          </p></div>
          <div class="tab-pane" id="profile"><h4>Progression :</h4>
                <div class="progress">
                    <div class="progress-bar" style="width: 49.5%"><span class="sr-only">49.5% Complete</span></div>
                    <div class="progress-bar progress-bar-success" style="width: 0.5%"><span class="sr-only">0.5% Complete</span></div>
                    <div class="progress-bar progress-bar-info" style="width: 24.5%"><span class="sr-only">24.5% Complete</span></div>
                    <div class="progress-bar progress-bar-warning" style="width: 0.5%"><span class="sr-only">0.5% Complete</span></div>
                    <div class="progress-bar progress-bar-info" style="width: 12%"><span class="sr-only">12% Complete</span></div>
                    <div class="progress-bar progress-bar-danger" style="width: 0.5%"><span class="sr-only">0.5% Complete</span></div>
                    <div class="progress-bar progress-bar-info" style="width: 12%"><span class="sr-only">12% Complete</span></div>
                    <div class="progress-bar progress-bar-danger" style="width: 0.5%"><span class="sr-only">0.5% Complete</span></div>
                </div>
                <h4>Succés :</h4>
                <div class="accordion" id="accordion2">
                    <div class="accordion-group">
                        <div class="accordion-heading">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapseOne">
                                <span class="btn btn-success col-lg-12"><i class="glyphicon glyphicon-star-empty"> Succés n°1 </i></span>
                            </a>
                        </div>
                        <div id="collapseOne" class="accordion-body collapse">
                            <div class="accordion-inner col-lg-12 wellEd">Anim pariatur cliche...</div>
                        </div>
                    </div>
                    <div class="accordion-group">
                        <div class="accordion-heading">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapseTwo">
                                <span class="btn btn-warning col-lg-12"><i class="glyphicon glyphicon-star-empty"> Succés n°2 </i></span>
                            </a>
                        </div>
                        <div id="collapseTwo" class="accordion-body collapse">
                            <div class="accordion-inner col-lg-12 wellEd">Anim pariatur cliche...</div>
                        </div>
                    </div>
                    <div class="accordion-group">
                        <div class="accordion-heading">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapseTree">
                                <span class="btn btn-danger col-lg-12"><i class="glyphicon glyphicon-star-empty"> Succés n°3 </i></span>
                            </a>
                        </div>
                        <div id="collapseTree" class="accordion-body collapse">
                            <div class="accordion-inner col-lg-12 wellEd">Anim pariatur cliche...</div>
                        </div>
                    </div>
                    <div class="accordion-group">
                        <div class="accordion-heading">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapseFour">
                                <span class="btn btn-danger col-lg-12"><i class="glyphicon glyphicon-star-empty"> Succés n°4 </i></span>
                            </a>
                        </div>
                        <div id="collapseFour" class="accordion-body collapse">
                            <div class="accordion-inner col-lg-12 wellEd">Anim pariatur cliche...</div>
                        </div>
                    </div>
                </div>
          </div>
        </div>
        
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Lancer l'exercice </button>
      </div>
    </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div><!-- /.modal -->
     <div class="modal fade" id="desc" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
        <h4 class="modal-title" id="descr">Orienté objet</h4>
      </div>
      <div class="modal-body">
          <h4>Description : </h4>
          <p>
              Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
          </p>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
      </div>
    </div>
  </div><!-- /.modal-dialog -->
</div><!-- /.modal -->
</asp:Content>
