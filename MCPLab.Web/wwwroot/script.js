// ------------------------------
// Enviar pergunta
// ------------------------------
document.getElementById("btnAsk").addEventListener("click", ask);
async function ask() {
    const c = document.getElementById("DdlCity").value;
    const q = document.getElementById("question").value;

    const res = await fetch("http://localhost:5044/api/ask", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ question: c + "-" + q })
    });

    const data = await res.json();
    document.getElementById("answer").innerText = data.answer;
}

// ------------------------------
// Tema claro/escuro
// ------------------------------
const themeSwitcher = document.getElementById("themeSwitcher");
const themeIcon = document.getElementById("themeIcon");
themeSwitcher.addEventListener("change", () => {
    if (themeSwitcher.checked) {
        document.body.classList.remove("dark");
        document.body.classList.add("light");
        themeIcon.classList.remove("fa-moon");
        themeIcon.classList.add("fa-sun");
    } else {
        document.body.classList.remove("light");
        document.body.classList.add("dark");
        themeIcon.classList.remove("fa-sun");
        themeIcon.classList.add("fa-moon");
    }
});

// ------------------------------
// Loading
// ------------------------------
async function ask() {
    const loading = document.getElementById("loading");
    const answer = document.getElementById("answer");

    loading.classList.remove("hidden");
    answer.innerText = "";

    const c = document.getElementById("DdlCity").value;
    const q = document.getElementById("question").value;

    const res = await fetch("http://localhost:5044/api/ask", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ question: c + "-" + q })
    });

    const data = await res.json();

    loading.classList.add("hidden");
    answer.innerText = data.answer;
}

// ------------------------------
// Esfera 3D de palavras
// ------------------------------
const sphere = document.getElementById("sphere");

// Lista de países
const countries = [
    "Brasil", "Argentina", "Chile", "Uruguai", "Paraguai", "Bolívia", "Peru", "Colômbia",
    "Venezuela", "México", "EUA", "Canadá", "Portugal", "Espanha", "França", "Alemanha",
    "Itália", "Inglaterra", "Irlanda", "Suécia", "Noruega", "Finlândia", "Rússia",
    "China", "Japão", "Coreia", "Índia", "Austrália", "Nova Zelândia", "Egito",
    "África do Sul", "Nigéria", "Marrocos", "Arábia Saudita", "Turquia"
];

// Distribuir palavras na esfera
const total = countries.length;
const radius = 120;
let words = [];

countries.forEach((country, i) => {
    const phi = Math.acos(-1 + (2 * i) / total);
    const theta = Math.sqrt(total * Math.PI) * phi;

    const x = radius * Math.cos(theta) * Math.sin(phi);
    const y = radius * Math.sin(theta) * Math.sin(phi);
    const z = radius * Math.cos(phi);

    const el = document.createElement("div");
    el.className = "word";
    el.innerText = country;

    el.style.transform = `translate3d(${x}px, ${y}px, ${z}px)`;

    sphere.appendChild(el);

    words.push({ el, x, y, z });
});

// Rotação automática + mouse
let rotX = 0.002;
let rotY = 0.002;
let mouseInfluenceX = 0;
let mouseInfluenceY = 0;

document.addEventListener("mousemove", (e) => {
    mouseInfluenceY = (e.clientX / window.innerWidth - 0.5) * 0.02;
    mouseInfluenceX = (e.clientY / window.innerHeight - 0.5) * 0.02;
});

function animateSphere() {
    rotX += mouseInfluenceX;
    rotY += mouseInfluenceY;
    words.forEach(w => {
        // Rotação em Y
        let x = w.x * Math.cos(rotY) - w.z * Math.sin(rotY);
        let z = w.x * Math.sin(rotY) + w.z * Math.cos(rotY);

        // Rotação em X
        let y = w.y * Math.cos(rotX) - z * Math.sin(rotX);
        z = w.y * Math.sin(rotX) + z * Math.cos(rotX);

        w.el.style.transform = `translate3d(${x}px, ${y}px, ${z}px)`;
    });

    requestAnimationFrame(animateSphere);
}

animateSphere();