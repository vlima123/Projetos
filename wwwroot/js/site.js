// Encontra os elementos necessários
// const btnVerResposta = document.getElementById('ver-resposta');
// const btnVerPergunta = document.getElementById('ver-pergunta');
// const flashcard = document.querySelector('.flashcard');

// btnVerResposta.addEventListener('click', () => {
//   flashcard.classList.add('show-answer');
// });

// btnVerPergunta.addEventListener('click', () => {
//   flashcard.classList.remove('show-answer');
// });

//flash card virar


const projetoSelect = document.getElementById('projeto-select');
const notasSelectContainer = document.getElementById('notas-select-container');
const projetoSelectContainer = document.getElementById('projeto-select-container');
const objetivoSelectContainer = document.getElementById('objetivos-select-container');
const objetivoSelect = document.getElementById('objetivos-select');
const notasSelect = document.getElementById('notas-select');
const projetoRadio = document.getElementById('projeto-radio');
const notasRadio = document.getElementById('notas-radio');
const objetivoRadio = document.getElementById('objetivos-radio');

projetoRadio.addEventListener('click', () => {
  projetoSelectContainer.style.display = 'block';
    console.log("click");
    notasSelectContainer.style.display = 'none';
    objetivoSelectContainer.style.display = 'none';
    objetivoSelect.selectedIndex = 0;
    notasSelect.selectedIndex = 0;
});

notasRadio.addEventListener('click', () => {
  projetoSelectContainer.style.display = 'none';
    notasSelectContainer.style.display = 'block';
    objetivoSelectContainer.style.display = 'none';
    objetivoSelect.selectedIndex = 0;
    projetoSelect.selectedIndex = 0;
});

objetivoRadio.addEventListener('click', () => {
  projetoSelectContainer.style.display = 'none';
    notasSelectContainer.style.display = 'none';
    objetivoSelectContainer.style.display = 'block';
    projetoSelect.selectedIndex = 0;
    notasSelect.selectedIndex = 0;
});

