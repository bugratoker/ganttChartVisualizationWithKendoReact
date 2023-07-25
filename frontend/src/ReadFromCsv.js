import React from 'react'

function ReadFromCsv() {
    const fileInput = document.getElementById("csvFileInput");
    const outputDiv = document.getElementById("output");
    function importBtn() {
        const file = fileInput.files[0];
        if (file) {
          importCsv(file);
        } else {
          outputDiv.textContent = "Please select a CSV file to import.";
        }
      };
    function importCsv(file){
        const reader = new FileReader();
        reader.onload = function(e) {
          const content = e.target.result;
          const lines = content.split("\n");
          const data = lines.map(line => line.split(","));
          // Now "data" contains your CSV data in a 2D array
          // You can process or display the data as needed here
          // For example, to display the data in the "outputDiv":
          outputDiv.textContent = JSON.stringify(data);
        };
        reader.readAsText(file);
        
    }
  return (
    <div>
    <input type="file" id="csvFileInput" accept=".csv"/>
    <button id="importButton" onClick={importBtn()}>Import CSV</button>
    <div id="output"></div>


    </div>
  )
}

export default ReadFromCsv