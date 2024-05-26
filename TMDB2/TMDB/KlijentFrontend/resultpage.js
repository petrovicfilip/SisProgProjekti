export class ResultPage
{
    constructor(results)
    {
        this.results = results;
    }

    draw(container)
    {
        this.results.forEach(element => 
        {
            let movieBox = document.createElement("div");
            movieBox.classList.add("movie-box"); 
            
            let naslov = document.createElement("div");
            let ocena = document.createElement("div");
            naslov.innerHTML = element["Title"] + " "+ element["Release_date"];
            ocena.innerHTML = "Ocena: " + element["Rating"];
            ocena.classList.add("naslov-i-ocena");
            naslov.classList.add("naslov-i-ocena");

            let opisislika = document.createElement("div");
            opisislika.classList.add("opis-i-slika");

            let slika = document.createElement("div");
            slika.classList.add("slikaplusopis");
            let img = document.createElement("img");
            img.src = "https://image.tmdb.org/t/p/w500" + element["Poster"];
            slika.appendChild(img);
            opisislika.appendChild(slika);

            let opis = document.createElement("div");
            opis.classList.add("slikaplusopis");
            opis.innerHTML = element["Description"];
            opisislika.appendChild(opis);

            movieBox.appendChild(naslov);
            movieBox.appendChild(ocena);
            movieBox.appendChild(opisislika);

            container.appendChild(movieBox);
        });
    }
}