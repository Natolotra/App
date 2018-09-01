function editerContratSante()
{
    window.open("PageImpression.aspx?param=contratSante", "Popup7", "toolbar=no,status=no,directories=no,menubar=no,location=no,scrollbars=yes,resizable=yes,height=400,top=10,left=400,width=600");
}

function editerAvenantPrecision()
{
    window.open("PageImpression.aspx?param=avenantPrecision", "Popup7", "toolbar=no,status=no,directories=no,menubar=no,location=no,scrollbars=yes,resizable=yes,height=400,top=10,left=400,width=600");
}

function editerAvenantRenouvellement()
{
    window.open("PageImpression.aspx?param=avenantRenouvellement", "Popup7", "toolbar=no,status=no,directories=no,menubar=no,location=no,scrollbars=yes,resizable=yes,height=400,top=10,left=400,width=600");
}

function editerAvenantSuspension()
{
    window.open("PageImpression.aspx?param=avenantSuspension", "Popup7", "toolbar=no,status=no,directories=no,menubar=no,location=no,scrollbars=yes,resizable=yes,height=400,top=10,left=400,width=600");
}

function editerAvenantResiliation()
{
    window.open("PageImpression.aspx?param=avenantResiliation", "Popup7", "toolbar=no,status=no,directories=no,menubar=no,location=no,scrollbars=yes,resizable=yes,height=400,top=10,left=400,width=600");
}

function editerAvenantRemiseEnVigueur()
{
    window.open("PageImpression.aspx?param=avenantRemiseEnVigueur", "Popup7", "toolbar=no,status=no,directories=no,menubar=no,location=no,scrollbars=yes,resizable=yes,height=400,top=10,left=400,width=600");
}

function editerRecu()
{
    window.open("PageImpression.aspx?param=recu", "Popup7", "toolbar=no,status=no,directories=no,menubar=no,location=no,scrollbars=yes,resizable=yes,height=400,top=10,left=400,width=600");
}

function CVSAdherentParticipant()
{
    window.open("PageCVS.aspx?param=AdherentParicipant", "Popup7", "toolbar=no,status=no,directories=no,menubar=no,location=no,scrollbars=yes,resizable=yes,height=400,top=10,left=400,width=600");
}
function CVSAdherent()
{
    window.open("PageCVS.aspx?param=Adherent", "Popup7", "toolbar=no,status=no,directories=no,menubar=no,location=no,scrollbars=yes,resizable=yes,height=400,top=10,left=400,width=600");
}

function majHorloge()
			{
				var div=document.getElementById("horloge");
				date=new Date();
				annees=date.getFullYear();
				mois=date.getMonth()+1;
				jour=date.getDate();
				heure=date.getHours();
				minute=date.getMinutes();
				seconde=date.getSeconds();
				var tab=new Array(annees,mois,jour,heure,minute,seconde);
				for(var i=0;i<=tab.length;i++)
				{
					if (tab[i]<10)
					{
						tab[i]="0"+tab[i];
					}
				}
				
				var mois;
				
				if(tab[1]==1)
				    mois = "Janvier";
				else if(tab[1]==2)
				    mois = "Février";
			    else if(tab[1]==3)
				    mois = "Mars";
				else if(tab[1]==4)
				    mois = "Avril";
				else if(tab[1]==5)
				    mois = "Mai";
				else if(tab[1]==6)
				    mois = "Juin";
				else if(tab[1]==7)
				    mois = "Juillet";
				else if(tab[1]==8)
				    mois = "Août";
			    else if(tab[1]==9)
				    mois = "Septembre";
				else if(tab[1]==10)
				    mois = "Octobre";
				else if(tab[1]==11)
				    mois = "Novembre";
				else if(tab[1]==12)
				    mois = "Décembre";
				
				div.innerHTML= "<b>" + tab[2] + " " + mois + " " + tab[0] + "<br/>" + tab[3] + ":" + tab[4] + ":" + tab[5] + "</b>";
				window.setTimeout(majHorloge,1000)
			}
			majHorloge();