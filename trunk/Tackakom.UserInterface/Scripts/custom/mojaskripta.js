
var naslov = '<input type="text" name="naslov" id="naslov" placeholder="Naslov događaja..." class="text ui-widget-content ui-corner-all" /> <br/> <br/>';
var opis = '<textarea id="opis" placeholder="Opis događaja..." class="text ui-widget-content ui-corner-all"></textarea><br/><br/>';
var datum = '<input type="text" name="datum" id="datum" class="text ui-widget-content ui-corner-all" placeholder="Izaberite datum..." /> <br/> <br/>';
var pocetak = '<input type="text" name="pocetak" id="pocetak" placeholder="Vreme početka..." class="text ui-widget-content ui-corner-all" /> <br/> <br/>';
var kraj = '<input type="text" name="kra" id="kra" placeholder="Vreme kraja..." class="text ui-widget-content ui-corner-all" /> <br/> <br/>';
var slika = '<img onclick="ChangeImage()" id="slika" src="../../Content/Slike/izaberi.jpg" />';

function getEvent() {
    naslovVR = $("#naslov").attr("value");
    opisVR = $("#opis").attr("value");
    datumVR = $("#datum").attr("value");
    pocetakVR = $("#pocetak").attr("value");
    krajVR = $("#kra").attr("value");
    return (naslovVR == "") ? null : { 
        Title: naslovVR,
        Description: opisVR,
        CreateTime: "6/4/2009",
        StartDate: "6/4/2009",
        EndDate: "6/4/2009",
        Entry: "Slobodno",  
        EventCategory: { Id: 1 }    
    };
}



var naslovVR = "";
var opisVR = "";
var datumVR = "";
var pocetakVR = "";
var krajVR = "";
var slikaVR = "";

var slika1 = '<img onclick="ChoseImage(1)" id="img1" class = "slika" src="../../Content/Slike/Ikonice/kockaste/fingerfocus.png" />';
var slika2 = '<img onclick="ChoseImage(2)" id="img2" class = "slika" src="../../Content/Slike/Ikonice/kockaste/fxphotostudio.png" />';
var slika3 = '<img onclick="ChoseImage(3)" id="img3" class = "slika" src="../../Content/Slike/Ikonice/kockaste/halftone.png" />';
var slika4 = '<img onclick="ChoseImage(4)" id="img4" class = "slika" src="../../Content/Slike/Ikonice/kockaste/lens.png" />';
var slika5 = '<img onclick="ChoseImage(5)" id="img5" class = "slika" src="../../Content/Slike/Ikonice/kockaste/lithogram.png" />';
var slika6 = '<img onclick="ChoseImage(6)" id="img6" class = "slika" src="../../Content/Slike/Ikonice/kockaste/pano.png" />';
var slika7 = '<img onclick="ChoseImage(7)" id="img7" class = "slika" src="../../Content/Slike/Ikonice/kockaste/perfectphoto.png" />';
var slika8 = '<img onclick="ChoseImage(8)" id="img8" class = "slika" src="../../Content/Slike/Ikonice/kockaste/pixrl.png" />';
var slika9 = '<img onclick="ChoseImage(9)" id="img9" class = "slika" src="../../Content/Slike/Ikonice/kockaste/photosynth.png" />';
var slika10 = '<img onclick="ChoseImage(10)" id="img10" class = "slika" src="../../Content/Slike/Ikonice/kockaste/wordfoto.png" />';


//DODAJ NOVI------------------------------------------------------------------------------------------------
function AddStuff() {
	$.fx.speeds._default = 1000;
	var $dialog = $('<div></div>')
	//SADRZAJ DIJALOGA
			.html
			(
			'<form>' + naslov + opis + slika + datum + pocetak + kraj + '</form>'
			)

	//OPCIJE DIJALOGA
			.dialog
			({
			    autoOpen: false,
			    show: "explode",
			    hide: "explode",
			    title: "Dodaj novi događaj",
			    modal: true,
			    width: 500,
			    height: 490,
			    buttons:
					{
					    "Dodaj događaj": function () {
					        
					        var event = getEvent();
					        if (event == null) {
					            alert("Upisi ime dogadjaja");
					            return;
					        }
					        var json = $.toJSON(event);
					        $.ajax({
					            url: '/Event/Save',
					            type: 'POST',
					            dataType: 'json',
					            data: json,
					            contentType: 'application/json; charset=utf-8',
					            success: function (data) {
					                var message = data.Message;
					                //$("#obavestenje").html(message);
					            }
					        });
					        $(this).dialog("close");
					        $(this).dialog("destroy").remove();
					    },
					    "Izađi": function () {
					        $(this).dialog("close");
					        $(this).dialog("destroy").remove();
					    }
					}

			});

		$dialog.dialog('open');
		//return false; <---- ????????

		//DATE PICK
		$(function () {
			$.datepicker.setDefaults($.datepicker.regional["sr-SR"]);
			$("#datum").datepicker({ minDate: 0, maxDate: "+2M" });
			$("#datum").datepicker("option", "dateFormat", "DD, d MM, yy");
			$("#datum").datepicker("option", "showAnim", "drop");
		});
	};

//EDIT-------------------------------------------------------------------------------------------------
   
	var editEnable = false;

	function Edit() {
		if (editEnable == true) {
			EditStuff();
		}
		else {
		   selectedError();
		}
	};

	function EditStuff() {
		$.fx.speeds._default = 1000;
		var $dialog = $('<div></div>')
		//SADRZAJ DIJALOGA
			.html
			(
			'<form>' + naslov + opis + slika + datum + pocetak + kraj + '</form>'
			)

		//OPCIJE DIJALOGA
			.dialog
			({
				autoOpen: false,
				show: "explode",
				hide: "explode",
				title: "Izmeni događaj",
				modal: true,
				width: 500,
				height: 490,
				buttons:
					{
						"Izmeni događaj": function () {
							Change();
							editEnable = false;
							deleteEnable = false;
							$(this).dialog("close");
							$(this).dialog("destroy").remove();
						},
						"Izađi": function () {
							$(this).dialog("close");
							$(this).dialog("destroy").remove();
						}
					}

			});

				$dialog.dialog('open');

		//DATE PICK***********************************************
		$(function () {
			$.datepicker.setDefaults($.datepicker.regional["sr-SR"]);
			$("#datum").datepicker({ minDate: 0, maxDate: "+2M" });
			$("#datum").datepicker("option", "dateFormat", "DD, d MM, yy");
			$("#datum").datepicker("option", "showAnim", "drop");
		});
		//********************************************************
		$("#naslov").attr("value", naslovVR);
		$("#opis").html(opisVR);
		$("#datum").attr("value", datumVR);
		$("#pocetak").attr("value", pocetakVR);
		$("#kra").attr("value", krajVR);
		$("#slika").attr("src", slikaVR);
	};

	//DELETE-------------------------------------------------------------------------------------------------
	var deleteEnable = false;

	function Delete() {
		if (deleteEnable == true) {
			DeleteStuff();
		}
		else {
			selectedError();
		}
	};

	function DeleteStuff() {
		$.fx.speeds._default = 1000;
		var $dialog = $('<div></div>')
		//SADRZAJ DIJALOGA
			.html
			(
			'<p><span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 20px 0;"></span>Ovaj događaj će biti zauvek obrisan. Jeste li sigurni da želite da ga obrišete?</p>'
			)

		//OPCIJE DIJALOGA
			.dialog
			({
				resizable: false,
				height: 160,
				show: "explode",
				hide: "explode",
				modal: true,
				title: "Obriši događaj?",
				buttons: {
					"Obriši događaj": function () {
						deleteEnable = false;
						editEnable = false;
						$(this).dialog("close");
						$(this).dialog("destroy").remove();
					},
					"Izađi": function () {
						$(this).dialog("close");
						$(this).dialog("destroy").remove();
					}
				}

			});

		$dialog.dialog('open');
	};


	//SELEKTOVANJE-------------------------------------------------------------------------------------------
	var idGlob;
	function SelectEvent(id, count) {
		idGlob = id;
		editEnable = true;
		deleteEnable = true;
		var brojEventa = count;
		for (var i=1; i <= brojEventa;i++)
		{
			if (id == i) {
				$("#" + i).css("background-color", "#34251B");
				$("#" + i).css("color", "white");
				$("#" + i).css("border-radius", "15px");
				$("#" + i).css("box-shadow", "0 0 5px 3px #FC6B11");

				naslovVR = $("#" + id + " h1").html();
				opisVR = $("#" + id + " p").html();
				datumVR = $("#" + id + " em.datum").html();
				pocetakVR = $("#" + id + " em.poc").html();
				krajVR = $("#" + id + " em.kraj").html();
				slikaVR = $("#" + id + " img").attr("src");
			}
			else {
				$("#" + i).css("background-color", "");
				$("#" + i).css("color", "");
				$("#" + i).css("border-radius", "");
				$("#" + i).css("box-shadow", "");
			}
		}
	};

	//NIJE SELEKTOVAN-------------------------------------------------------------------------------------------
	function selectedError() {
		
		var $dialog = $('<div></div>')
		//SADRZAJ DIJALOGA
			.html
			(
			'<p><span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 20px 0;"></span>Morate prvo da selektujte neki događaj!</p>'
			)

		//OPCIJE DIJALOGA
			.dialog
			({
				resizable: false,
				height: 140,
				show: "explode",
				hide: "explode",
				modal: true,
				title: "Greška!",
				buttons: {
					"Uredu": function () {
						$(this).dialog("close");
						$(this).dialog("destroy").remove();
					}
				}
			});

		$dialog.dialog('open');
	}

	//CHANGE EVENT----------------------------------------------------------------------------------
	function Change() {
		naslovVR = $("#naslov").attr("value");
		opisVR = $("#opis").attr("value");
		datumVR = $("#datum").attr("value");
		pocetakVR = $("#pocetak").attr("value");
		krajVR = $("#kra").attr("value");
		slikaVR = $("#slika").attr("src");

		$("#" + idGlob).css("background-color", "");
		$("#" + idGlob).css("color", "");
		$("#" + idGlob).css("border-radius", "");
		$("#" + idGlob).css("box-shadow", "");

		$("#" + idGlob + " h1").html(naslovVR);
		$("#" + idGlob + " p").html(opisVR);
		$("#" + idGlob + " em.datum").html(datumVR);
		$("#" + idGlob + " em.poc").html(pocetakVR);
		$("#" + idGlob + " em.kraj").html(krajVR);
		$("#" + idGlob + " img").attr("src", slikaVR);
	};


	//CHANGE IMAGE------------------------------------------------------------------------------------
	function ChangeImage() {
		$(this).dialog("close");

		$.fx.speeds._default = 1000;
		var $dialog = $('<div></div>')
		//SADRZAJ DIJALOGA
			.html
			(
			slika1 + slika2 + slika3 + slika4 + slika5 + slika6 + slika7 + slika8 + slika9 + slika10
			)

		//OPCIJE DIJALOGA
			.dialog
			({
				resizable: false,
				modal: true,
				title: "Izaberite sliku",
				width: 450,
				buttons: {
					"Izaberi": function () {
						$(this).dialog("close");
						$(this).dialog("destroy").remove();
					},
					"Izađi": function () {
						$(this).dialog("close");

					}
				}

			});

		$dialog.dialog('open');

	};

	//CHOSE IMAGE---------------------------------------------------------------------------------

	function ChoseImage(id) {
		for (var i=1;i<11;i++)
		{
			if (id == i) {
				$("#" + "img" + i).css("background-color", "#34251B");
				$("#" + "img" + i).css("border-radius", "15px");
				$("#" + "img" + i).css("box-shadow", "0 0 5px 5px #FC6B11");

				slikaVR = $("#" + "img" + id).attr("src");
				$("#slika").attr("src", slikaVR);
			}
			else {
				$("#" + "img" + i).css("background-color", "");
				$("#" + "img" + i).css("border-radius", "");
				$("#" + "img" + i).css("box-shadow", "");
			}
		}
};
(function ($) {
    m = {
        '\b': '\\b',
        '\t': '\\t',
        '\n': '\\n',
        '\f': '\\f',
        '\r': '\\r',
        '"': '\\"',
        '\\': '\\\\'
    },
	$.toJSON = function (value, whitelist) {
	    var a,          // The array holding the partial texts.
			i,          // The loop counter.
			k,          // The member key.
			l,          // Length.
			r = /["\\\x00-\x1f\x7f-\x9f]/g,
			v;          // The member value.

	    switch (typeof value) {
	        case 'string':
	            return r.test(value) ?
				'"' + value.replace(r, function (a) {
				    var c = m[a];
				    if (c) {
				        return c;
				    }
				    c = a.charCodeAt();
				    return '\\u00' + Math.floor(c / 16).toString(16) + (c % 16).toString(16);
				}) + '"' :
				'"' + value + '"';

	        case 'number':
	            return isFinite(value) ? String(value) : 'null';

	        case 'boolean':
	        case 'null':
	            return String(value);

	        case 'object':
	            if (!value) {
	                return 'null';
	            }
	            if (typeof value.toJSON === 'function') {
	                return $.toJSON(value.toJSON());
	            }
	            a = [];
	            if (typeof value.length === 'number' &&
					!(value.propertyIsEnumerable('length'))) {
	                l = value.length;
	                for (i = 0; i < l; i += 1) {
	                    a.push($.toJSON(value[i], whitelist) || 'null');
	                }
	                return '[' + a.join(',') + ']';
	            }
	            if (whitelist) {
	                l = whitelist.length;
	                for (i = 0; i < l; i += 1) {
	                    k = whitelist[i];
	                    if (typeof k === 'string') {
	                        v = $.toJSON(value[k], whitelist);
	                        if (v) {
	                            a.push($.toJSON(k) + ':' + v);
	                        }
	                    }
	                }
	            } else {
	                for (k in value) {
	                    if (typeof k === 'string') {
	                        v = $.toJSON(value[k], whitelist);
	                        if (v) {
	                            a.push($.toJSON(k) + ':' + v);
	                        }
	                    }
	                }
	            }
	            return '{' + a.join(',') + '}';
	    }
	};

})(jQuery);