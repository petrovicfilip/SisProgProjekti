import { ResultPage } from "./resultpage.js";

export class Application
{
    constructor()
    {}
    search = {
        naziv : "Naziv:",
        klasa : "naziv-class"
    };

    find = {
        naziv : "IMDB ID:",
        klasa : "imdb-class"
    };

    discover = [
    {naziv:"Include adult:",klasa:"include_adult"},
    {naziv:"Include video:",klasa:"include_video"},
    {naziv:"Language:",klasa:"language"}
    ];

    draw(container)
    {
        let cont = document.createElement("div");

        let divSearch = document.createElement("div");
        let divFind = document.createElement("div");
        let divDiscover = document.createElement("div");

        cont.classList.add("father");
        divSearch.classList.add("search");
        divFind.classList.add("find");
        divDiscover.classList.add("discover");

        this.drawSearch(divSearch,container);
        this.drawFind(divFind,container);
        this.drawDiscover(divDiscover,container);


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
        naslov.style.borderBottom = "3px solid aqua"
        naslov.classList.add("search-naslov");

        let elements = document.createElement("div");
        elements.classList.add("elementi-search");

        let label = document.createElement("label");
        label.innerHTML = this.search.naziv;
        label.style.margin = "10px";
        let search = document.createElement("input");
        search.style.margin = "10px";
        search.classList.add(`${this.search.klasa}`,"input-stil");
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
        naslov.style.borderBottom = "3px solid aqua"
        naslov.classList.add("search-naslov");

        let elements = document.createElement("div");
        elements.classList.add("elementi-search");

        let label = document.createElement("label");
        label.innerHTML = this.find.naziv;
        label.style.margin = "10px";
        let search = document.createElement("input");
        search.style.margin = "10px";
        search.classList.add(`${this.find.klasa}`,"input-stil");
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
    drawDiscover(container)
    {
        let naslov = document.createElement("div");
        naslov.innerHTML = "Discover";
        naslov.style.fontSize = "25px"  
        naslov.style.borderBottom = "3px solid aqua"
        naslov.classList.add("search-naslov");

        let elements = document.createElement("div");
        elements.classList.add("elementi-search");

        
        this.discover.forEach(p =>
        {
            if(p.naziv == "Include adult:" || p.naziv == "Include video:")
            {
                let label = document.createElement("label");
                label.innerHTML = p.naziv;
                label.style.margin = "10px";

                let select = document.createElement("select");
                select.style.margin = "10px";
                select.classList.add(`${p.klasa}`);

                let t = document.createElement("option");
                t.innerHTML = "true";
                let f = document.createElement("option");
                f.innerHTML = "false";

                select.appendChild(t);
                select.appendChild(f);

                elements.appendChild(label);
                elements.appendChild(select);
            }
            else
            {
                let label = document.createElement("label");
                label.innerHTML = p.naziv;
                label.style.margin = "10px";
                let search = document.createElement("input");
                search.style.margin = "10px";
                search.classList.add(`${p.klasa}`,"input-stil");

                elements.appendChild(label);
                elements.appendChild(search);
            }
        }
        );

        let btn = document.createElement("input");
        btn.style.margin = "10px";
        btn.value = "Posalji zahtev";
        btn.type = "button";
        btn.classList.add("buttons");
        btn.onclick = () => this.posalji(3);

        container.appendChild(naslov);
        container.appendChild(elements);
        container.appendChild(btn);
    }

    posalji = async (num) =>
    {
        let fetchh = "http://127.0.0.1:5500/" ;

        let father = document.querySelector(".results");

        if(father)
            father.replaceChildren();

        if(num == 1)
        {
            let search = fetchh + "search" + "/" + document.querySelector(`.${this.search.klasa}`).value;

            let result = await fetch(search).then(r => r.json());
            result.forEach(f =>
            {
                console.log(f["Title"]);
            }
            );

            if(!father)
                {
                    father = document.createElement("div");
                    father.classList.add("results");
                    document.body.appendChild(father);
                }
                let res = new ResultPage(result);
                res.draw(father);
        }
        else if(num == 2)
        {
			let find = fetchh + "find" + "/" + document.querySelector(`.${this.find.klasa}`).value;
            let result = await fetch(find).then(r => r.json());
            result.forEach(f =>
            {
                console.log(f["Title"]);
            }
            );

            if(!father)
                {
                    father = document.createElement("div");
                    father.classList.add("results");
                    document.body.appendChild(father);
                }
                let res = new ResultPage(result);
                res.draw(father);
        }
        else
        {
            let discover = fetchh + "discover/";

            this.discover.forEach((p,index) =>
                {
                    discover += p.klasa + "=" + document.querySelector(`.${p.klasa}`).value;
                    if(index < this.discover.length - 1)
                        discover += "&";
                }
            );
            let result = await fetch(discover).then(r => r.json());
            result.forEach(f =>
            {
                console.log(f["Title"]);
            }
            );

            if(!father)
            {
                father = document.createElement("div");
                father.classList.add("results");
                document.body.appendChild(father);
            }
            let res = new ResultPage(result);
            res.draw(father);
		}
    }        
}