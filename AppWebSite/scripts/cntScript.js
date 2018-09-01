


/*****************horloge******************************/
function majHorloge()
			{
				var div=document.getElementById("horloge2");
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

				div.innerHTML = "<b>" + tab[2] + " " + mois + " " + tab[0] + " - " + tab[3] + ":" + tab[4] + ":" + tab[5] + "<br/>";
				window.setTimeout(majHorloge,1000)
			}
			majHorloge();
/********************fin horloge*********************/