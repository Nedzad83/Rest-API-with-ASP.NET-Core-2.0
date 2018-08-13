$(function() {
	$('#run_launcher input').addClass('hidden');
	$('#run_launcher h3').click(function() {    
		$('#run_launcher input').toggleClass('hidden');  
	}); 
});

$(function() {
	$('#switcher h3').click(function() {
		$('#switcher button').toggleClass('hidden');
	});
});

$(function() {
	$('#control_env_div').addClass('hidden');
	$('#control_env_div h3').click(function() {    
		$('#control_env_div a').toggleClass('hidden');  
	}); 
});