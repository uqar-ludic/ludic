<%@ Page Title="Edition" Language="C#" MasterPageFile="~/SandBox.Master" AutoEventWireup="true" CodeBehind="Edition.aspx.cs" Inherits="Projet_Libre.Edition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/Edition.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LeftContent" runat="server">
    <div>
        <div class="panel-group" id="accordion">
            <ul class="nav nav-list" id="colEdition">
                <li>
                    <a href="#" data-toggle="collapse" data-target="#coExo" data-parent="#accordion">
                        <i class="glyphicon glyphicon-home"></i>
                         Listes des exercices 
                        <i class="glyphicon glyphicon-chevron-down"></i>
                    </a>
                </li>
            </ul>
            <div id="coExo" class="collapse">
                <ul class="nav nav-list clearfix"> 
                    <li><a href="#prev"><i class="glyphicon glyphicon-arrow-left"></i> Exercice n° 42</a></li>
                    <li><a href="#act"><i class="glyphicon glyphicon-ok active"></i> Exercice n° 43</a></li>
                    <li><a href="#next"><i class="glyphicon glyphicon-arrow-right"></i> Exercice n° 44</a></li>
                    <li class="divider"></li>
                    <li><a href="#Exercices.apsx"><i class="glyphicon glyphicon-list"></i> Retour a la liste</a></li>
                </ul>
            </div>
        </div>
        <div id="actionEdition">
            <ul class="nav nav-list"> 
                <li><a href="Edition.aspx"><i class="glyphicon glyphicon-floppy-disk"></i> Enregistrer la solution</a></li>
                <li><a href="Edition.aspx"><i class="glyphicon glyphicon-flash"></i> Exécuter la solution</a></li>
            </ul>
        </div>
        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="margin-top:-15px;">Exercice n° <small style="color:#d2322d;">42</small></h2>
    <h3>Console : </h3>
    <div style="background-color:#5cb85c;border-radius:1px;padding:2px;">
        <textarea class="col-lg-12" style="height:250px;background:#333333; color:white;border-radius:15px;"></textarea>
    </div>
    <div id="tabEdition" class="btn-group full-width-tabs" data-toggle="buttons-radio">
        <ul class="nav nav-tabs">
            <li class="take-all-space-you-can active"><a href="#sorties" class="btn btn-block btn-success" data-toggle="tab">Sorties</a></li>
            <li class="take-all-space-you-can"><a href="#messages" class="btn btn-block btn-success" data-toggle="tab">Message d'erreur</a></li>
            <li class="take-all-space-you-can"><a href="#sujet" class="btn btn-block btn-success" data-toggle="tab">Sujet</a></li>
            <li class="take-all-space-you-can"><a href="#succés" class="btn btn-block btn-success"  data-toggle="tab">Succés</a></li>
            <li class="take-all-space-you-can"><a href="#commentaires" class="btn btn-block btn-success"  data-toggle="tab">Commentaires</a></li>
        </ul>
    </div>
    <div style="background-color:#5cb85c;padding:0px 2px 2px 2px;">
        <div class="tab-content" style="border-bottom-left-radius:2px;border-bottom-right-radius:2px;">
             <div class="tab-pane wellEd active" id="sorties">
            <h4>Sorties attendues</h4>
            <div style="background-color:#ed9c28;border-radius:1px;padding:2px;">
                <textarea class="col-lg-12" style="height:100px;background:#333333; color:white;border-radius:15px;"></textarea>
            </div>
            <h4>Sorties reçues</h4>
            <div style="background-color:#ed9c28;border-radius:1px;padding:2px;">
                <textarea class="col-lg-12" style="height:100px;background:#333333; color:white;border-radius:15px;"></textarea>
            </div>
        </div>
            <div class="tab-pane wellEd" id="messages">
                <table class="table table-striped">
                    <thead><tr><th>N° </th><th>Ligne</th><th>Message</th></tr></thead>
                    <tbody>
                        <tr><td>01</td><td>123 </td><td>error on ...</td></tr>
                        <tr><td>02</td><td>125 </td><td>error on ...</td></tr>
                        <tr><td>03</td><td>126 </td><td>error on ...</td></tr>
                    </tbody>
                </table>
            </div>
            <div class="tab-pane wellEd" id="sujet"><h4>Sujet</h4>
            </div>
            <div class="tab-pane wellEd" id="succés">
                <h4>Progression :</h4>
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
            <div class="tab-pane wellEd" id="commentaires">
		        <h4>Note pour l'exercice n°42</h4>
                <div id="stars" class="starrr"></div>
                Vous avez noté l'exercice sur <span id="count">0</span> étoile(s)
		        <h4>Vote commentaire : </h4>
                <div class="accordion" id="accordion3">
                    <div class="accordion-group">
                        <div class="accordion-heading">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion3" href="#collapseComment">
                                <span class="btn btn-warning col-lg-12" style="border-bottom-left-radius:0px;border-bottom-right-radius:0px;"><i class="glyphicon glyphicon-leaf"> Laisser votre commentaire </i></span>
                            </a>
                        </div>
                        <div id="collapseComment" class="accordion-body collapse">
                            <div class="accordion-inner col-lg-12 wellEd-orange">
                                <form accept-charset="UTF-8" method="post">
                                    <div><label>Titre : </label> <input class="form-control" placeholder="Titre..." name="titre" type="text"/></div>
                                    <textarea class="form-control" cols="50" id="new-review"  style="margin-top:10px;" name="comment" placeholder="Laisser votre commentaire..." rows="5"></textarea>
                                    <button class="btn btn-success center-block" type="submit" style="margin-top:10px;">Soumettre</button>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
                <h4>Commentaires : </h4>
                <div class="">
                    <div>
                        <span class=" label label-info">Alice in Wonderland, part dos</span>
                        <p>'You ought to be ashamed of yourself for asking such a simple question,' added the Gryphon; and then they both sat silent and looked at poor Alice, who felt ready to sink into the earth. At last the Gryphon said to the Mock Turtle, 'Drive on, old fellow! Don't be all day about it!' and he went on in these words:
                        'Yes, we went to school in the sea, though you mayn't believe it—'
                        'I never said I didn't!' interrupted Alice.
                        'You did,' said the Mock Turtle.</p>
                        <div>
                            <span class="badge">Posté par User à 2012-08-02 20:47:04</span>
                        </div>
                    </div>
                    <div>
                        <span class="label label-info">Revolution has begun!</span>
                        <p>'I am bound to Tahiti for more men.'
                            'Very good. Let me board you a moment—I come in peace.' With that he leaped from the canoe, swam to the boat; and climbing the gunwale, stood face to face with the captain.
                            'Cross your arms, sir; throw back your head. Now, repeat after me. As soon as Steelkilt leaves me, I swear to beach this boat on yonder island, and remain there six days. If I do not, may lightning strike me!'A pretty scholar,' laughed the Lakeman. 'Adios, Senor!' and leaping into the sea, he swam back to his comrades.</p>
                        <div>
                            <span class="badge">Posté par User à 2012-08-02 20:47:04</span>
                        </div>
                    </div>
                    <div class="pager" id="pager">
                        <ul>
                            <li><a href="#">Prev</a></li>
                            <li><a href="#">1</a></li>
                            <li><a href="#">2</a></li>
                            <li><a href="#">3</a></li>
                            <li><a href="#">4</a></li>
                            <li><a href="#">5</a></li>
                            <li><a href="#">Next</a></li>
                        </ul>
                    </div>
                </div>  
            </div>
        </div>
    </div>
</asp:Content>
