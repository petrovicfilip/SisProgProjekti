export class Application
{
    constructor()
    {

    }
    search = {
        naziv : "Naziv:",
        klasa : "naziv-class"
    };

    find = {
        naziv : "IMDB ID:",
        klasa : "imdb-class"
    };

    discover = [
    {naziv:"Include adult:",klasa:"include-adult-class"},
    {naziv:"Include video",klasa:"include-video-class"},
    {naziv:"Language:",klasa:"language-class"}
    ];

    draw(container)
    {
        let cont = document.createElement("div");

        let divSearch = document.createElement("div");
        //divSearch.innerHTML = "Kurac";
        let divFind = document.createElement("div");
        //divFind.innerHTML = "Picka";
        let divDiscover = document.createElement("div");
        //divDiscover.innerHTML = "Sisa";

        cont.classList.add("father");
        divSearch.classList.add("search");
        divFind.classList.add("find");
        divDiscover.classList.add("discover");

        this.drawSearch(divSearch);
        this.drawFind(divFind);
        this.drawDiscover(divDiscover);


        cont.appendChild(divSearch);
        cont.appendChild(divFind);
        cont.appendChild(divDiscover);

        container.appendChild(cont);
    }

    drawSearch(container)
    {
        let naslov = document.createElement("div");
        naslov.innerHTML = "Search";
        naslov.style.fontSize = "25px"
        naslov.style.borderBottom = "solid black"
        naslov.classList.add("search-naslov");

        let elements = document.createElement("div");
        elements.classList.add("elementi-search");

        let label = document.createElement("label");
        label.innerHTML = this.search.naziv;
        label.style.margin = "10px";
        let search = document.createElement("input");
        search.style.margin = "10px";
        search.classList.add(`${this.search.klasa}`);
        let btn = document.createElement("input");
        btn.style.margin = "10px";
        btn.value = "Posalji zahtev";
        btn.type = "button";
        btn.classList.add("buttons");
        btn.onclick = () => this.posalji(1);


        elements.appendChild(label);
        elements.appendChild(search);

        container.appendChild(naslov);
        container.appendChild(elements);
        container.appendChild(btn);
    }
    drawFind(container)
    {
        let naslov = document.createElement("div");
        naslov.innerHTML = "Find";
        naslov.style.fontSize = "25px"
        naslov.style.borderBottom = "solid black"
        naslov.classList.add("search-naslov");

        let elements = document.createElement("div");
        elements.classList.add("elementi-search");

        let label = document.createElement("label");
        label.innerHTML = this.find.naziv;
        label.style.margin = "10px";
        let search = document.createElement("input");
        search.style.margin = "10px";
        search.classList.add(`${this.find.klasa}`);
        let btn = document.createElement("input");
        btn.style.margin = "10px";
        btn.value = "Posalji zahtev";
        btn.type = "button";
        btn.classList.add("buttons");
        btn.onclick = () => this.posalji(2);


        elements.appendChild(label);
        elements.appendChild(search);

        container.appendChild(naslov);
        container.appendChild(elements);
        container.appendChild(btn);
              
    }
    drawDiscover(container){return;}

    posalji = async(num) =>
    {
        let fetchh = "http://127.0.0.1:5500/" ;
        if(num == 1)
        {
            let search = fetchh + "search" + "/" + document.querySelector(`.${this.search.klasa}`).value;

            //let result = await fetch(search).then(r => r.json);
            //console.log(result);
            window.location.href = search;

        }
        else if(num == 2)
        {
			let search = fetchh + "find" + "/" + document.querySelector(`.${this.find.klasa}`).value;
            window.location.href = search;
        }
        else
        {
			//...
		}
    }        
}