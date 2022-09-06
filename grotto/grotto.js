var post = document.getElementsByClassName("post")[0];
var content = post.children.item(2);

var modal = document.getElementById("modal");
var contentModal = document.getElementById("modal-content").children.item(1);
var shut = document.getElementById("close");
var body = document.body;

var start = document.getElementById("start");

var inModal = false;

post.onclick = function()
{
	print("post clicked");
	switch(inModal)
	{
		case false:
			print("post modal display");
			contentModal.textContent = content.textContent;
			modal.style.display = "block";
			inModal = true;
			break;
		case true:
			print("post modal hide");
			modal.style.display = "none";
			inModal = false;
			break;
	}
}

shut.onclick = function()
{
	print("x-button");
	if (inModal && modal.style.display == "block")
	{
		print("modal x'ed");
		modal.style.display = "none";
		inModal = false;
	}
}

start.onclick = function()
{
	if(!inModal)
	{
		inModal = true;
		contentModal.innerHTML = "<textarea id=\"post\" name=\"post\" placeholder=\"What\'s up?\" rows=\"5\" cols=\"50\"></textarea>"+"<input type=\"submit\" value=\"submit\" id=\"submit\">"
		modal.style.display = "block";
	}
}

function print(text) { console.log(text); }
