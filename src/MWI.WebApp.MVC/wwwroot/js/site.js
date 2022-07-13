function WizardSteps() {
	$(document).ready(function () {
		$('.step').hide();
		$('.step').first().show();

		var stepindex = 1;

		$('.steptitle' + stepindex).addClass("active");

		var stepnext = function () {
			$('.steptitle' + stepindex).removeClass("active")
			stepindex += 1;
			$('.steptitle' + stepindex).addClass("active");
		}

		var stepprev = function () {
			$('.steptitle' + stepindex).removeClass("active")
			stepindex -= 1;
			$('.steptitle' + stepindex).addClass("active");
		}

		var passoexibido = function () {
			var index = parseInt($('.step:visible').index());
			if (index == 0) {
				//Se for o primeiro passo desabilita o botão de voltar
				$("#prev").prop('disabled', true);
			} else if (index == (parseInt($('.step').length) - 1)) {
				//Se for o ultimo passo desabilita o botão de avançar
				$("#next").prop('disabled', true);
			} else {
				//Em outras situações os dois serão habilitados
				$("#next").prop('disabled', false);
				$("#prev").prop('disabled', false);
			}
		};

		passoexibido();

		$("#next").click(function () {
			$(".step:visible").hide().next().show();
			$('.steptitle:active').removeClass("active").next().addClass("active");
			passoexibido();
			stepnext();
		});

		//retrocede para o passo anterior
		$("#prev").click(function () {
			$(".step:visible").hide().prev().show();
			$('.steptitle:active').removeClass("active").prev().addClass("active");
			passoexibido();
			stepprev();
		});
	});
}