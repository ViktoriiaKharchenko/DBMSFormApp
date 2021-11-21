
var databasesList = [];
const links = document.querySelector("#links");
const dataPart = document.querySelector("#dataPart");
const actionsHolder = document.querySelector("#actionsHolder");
let linksList = [];
$(document).ready(function () {

    $.ajax({

        url: 'https://localhost:44377/api/Database',
        method: "GET",    
        success: function (data) {
            databasesList = data;
            console.log(data);
            setFirstData();
        }
    });
});


function updateLinks(name) {
    actionsHolder.innerHTML = '';
    links.innerHTML = '';
    let newLinkList = [];
    for (let i = 0; i < linksList.length; i++) {
        if (linksList[i] === name) {
            break;
        }
        let link = document.createElement("div");
        link.classList.add("link");
        link.innerText = linksList[i];
        links.appendChild(link);
        newLinkList.push(linksList[i]);
        addListener(link, i, linksList[i])
        if (linksList[i + 1] != name) {


            let separator = document.createElement("div");
            separator.classList.add("link_separator");
            separator.innerText = '>';
            links.appendChild(separator);
        }
    }
    linksList = newLinkList;
}

function addListener(link, i, name) {
    if (i === 0) {
        link.addEventListener('click', () => {
            updateLinks(name);
            setFirstData();
        });
    }
    if (i === 1) {
        link.addEventListener('click', () => {
            updateLinks(name);
            selectDatabase(name);
        });
    }
    if (i === 2) {
        link.addEventListener('click', () => {
            updateLinks(name);
            selectedTableInfo(name);
        });
    }
}
function setFirstData() {
    dataPart.innerHTML = '';
    let link = document.createElement("div");
    link.classList.add("link");
    link.innerText = 'Databases';
    link.addEventListener('click', () => {
        updateLinks('Databases');
        setFirstData();
    });
    linksList.push("Databases");
    links.appendChild(link);


    let table = document.createElement("table");
    dataPart.appendChild(table);

    let headerRow = document.createElement("tr");
    headerRow.id = 'tableHeaders';
    let header1 = document.createElement("th");
    header1.innerText = 'Database Name';
    headerRow.appendChild(header1);
    table.appendChild(headerRow);

    for (let i = 0; i < databasesList.length; i++) {
        let tableRow = document.createElement("tr");
        tableRow.classList.add("variant");
        let tableCell = document.createElement("td");
        tableCell.innerText = databasesList[i].name;
        tableRow.appendChild(tableCell);
        table.appendChild(tableRow);

        tableRow.addEventListener('click', () => {
            selectDatabase(databasesList[i].id);
        }
        )
    }
}

function addActionButtons(databaseId) {
    actionsHolder.innerHTML = "";
    let div = document.createElement("div");
    div.classList.add('actionButton');
    div.innerText = "Join tables";
    div.addEventListener('click', () => {
        createJoinForm(databaseId);
    });
    actionsHolder.appendChild(div);
}

function createJoinForm(databaseId) {
    dataPart.innerHTML = "";
    let filteredDataBaseList = databasesList.filter((element) => {
        return element.id === databaseId;
    });
    let tblLabel1 = document.createElement('label');
    tblLabel1.innerText = "First table";
    let selectTable1 = document.createElement("select");
    selectTable1.id = "table1";
    let tblLabel2 = document.createElement('label');
    tblLabel2.innerText = "Second table";
    let selectTable2 = document.createElement("select");
    selectTable2.id = "table2";
    for (let i = 0; i < filteredDataBaseList[0].tables.length; i++) {
        let option = document.createElement("option");
        let option2 = document.createElement("option");
        option.text = filteredDataBaseList[0].tables[i].name;
        option.value = filteredDataBaseList[0].tables[i].id;
        option2.text = filteredDataBaseList[0].tables[i].name;
        option2.value = filteredDataBaseList[0].tables[i].id;

        selectTable1.appendChild(option);
        selectTable2.appendChild(option2);
    }
    dataPart.appendChild(tblLabel1);
    dataPart.appendChild(selectTable1);
    dataPart.appendChild(tblLabel2);
    dataPart.appendChild(selectTable2);

    let colLabel1 = document.createElement('label');
    colLabel1.innerText = "First column";
    let selectColumn1 = document.createElement("select");
    selectColumn1.id = "col1";
    let colLabel2 = document.createElement('label');
    colLabel2.innerText = "Second column";
    let selectColumn2 = document.createElement("select");
    selectColumn2.id = "col2";
    if (filteredDataBaseList[0].tables.length > 0) {
        for (let i = 0; i < filteredDataBaseList[0].tables[0].columns.length; i++) {
            let option = document.createElement("option");
            let option2 = document.createElement("option");
            option.text = filteredDataBaseList[0].tables[0].columns[i].name;
            option.value = filteredDataBaseList[0].tables[0].columns[i].name;
            option2.text = filteredDataBaseList[0].tables[0].columns[i].name;
            option2.value = filteredDataBaseList[0].tables[0].columns[i].name;

            selectColumn1.appendChild(option);
            selectColumn2.appendChild(option2);
        }
    }
    dataPart.appendChild(colLabel1);
    dataPart.appendChild(selectColumn1);
    dataPart.appendChild(colLabel2);
    dataPart.appendChild(selectColumn2);

    let div = document.createElement("div");
    div.classList.add('actionButton');
    div.innerText = "Join";
    div.addEventListener('click', () => {
        join(databaseId, document.getElementById('table1').value, document.getElementById('table2').value, document.getElementById('col1').value, document.getElementById('col2').value);
    });
    dataPart.appendChild(div);

    $(document).ready(function () {
        $("#table1").change(function () {
            $.ajax({

                url: 'https://localhost:44377/api/database/' + databaseId+'/table/' + $("#table1").val()+' /column',
                method: "GET",
                success: function (data) {
                    console.log(data);
                    $("#col1").empty();
                    $.each(data, function (index, row) {
                        $("#col1").append("<option value='" + row.id + "'>" + row.name + "</option>")
                    });
                }
            });
        });
    });
    $(document).ready(function () {
        $("#table2").change(function () {
            $.ajax({

                url: 'https://localhost:44377/api/database/' + databaseId+'/table/' + $("#table2").val() + ' /column',
                method: "GET",
                success: function (data) {
                    console.log(data);
                    $("#col2").empty();
                    $.each(data, function (index, row) {
                        $("#col2").append("<option value='" + row.name + "'>" + row.name + "</option>")
                    });
                }
            });
        });
    });
}
function join(databaseId, tbl1, tbl2, col1, col2) {
 
    $(document).ready(function () {

        $.ajax({

            url: 'https://localhost:44377/api/database/' + databaseId+'/table/join',
            method: "post",
            contentType: 'application/json',
            data: JSON.stringify({
                    'table1': tbl1,
                    'table2': tbl2,
                    'col1': col1,
                    'col2': col2
            }),
            error: function (e) {
                console.log('error');
            },
            success: function (data) {
                console.log(data);
                generateJoinedTable(data);
            }
           
        });
    });
}
function generateJoinedTable(responce) {
    if (responce.statusCode == 500 || responce.statusCode == 400) {
        alert("Wrong selected data");
        return;
    }

    dataPart.innerHTML = '';
    let table = document.createElement("table");
    dataPart.appendChild(table);

    let headerRow = document.createElement("tr");
    headerRow.id = 'tableHeaders';
    for (let i = 0; i < responce.columns.length; i++) {
        let header1 = document.createElement("th");
        header1.innerText = responce.columns[i].name;
        headerRow.appendChild(header1);
    }
    table.appendChild(headerRow);

    for (let i = 0; i < responce.rows.length; i++) {
        let row = document.createElement("tr");
        row.classList.add("variant");
        for (let j = 0; j < responce.rows[i].length; j++) {
            let tableCell = document.createElement("td");
            row.appendChild(tableCell);

            tableCell.innerText = responce.rows[i][j];
        }
        table.appendChild(row);
    }
}
function selectDatabase(databaseId) {

    let filteredDataBaseList = databasesList.filter((element) => {
        return element.id === databaseId;
    });
    let currentDataBase;
    if (filteredDataBaseList.length > 0) {
        currentDataBase = filteredDataBaseList[0];
    }

    let separator = document.createElement("div");
    separator.classList.add("link_separator");
    addActionButtons(databaseId);
    separator.innerText = '>';
    links.appendChild(separator);
    console.log(databaseId);
    let link = document.createElement("div");
    link.classList.add("link");
    link.innerText = currentDataBase.name;
    link.addEventListener('click', () => {
        updateLinks(databaseId);
        selectDatabase(databaseId);
    });
    linksList.push(databaseId);
    links.appendChild(link);

    dataPart.innerHTML = '';


    let table = document.createElement("table");
    dataPart.appendChild(table);

    let headerRow = document.createElement("tr");
    headerRow.id = 'tableHeaders';
    let header1 = document.createElement("th");
    header1.innerText = 'Table Name';
    headerRow.appendChild(header1);
    table.appendChild(headerRow);

    for (let i = 0; i < currentDataBase.tables.length; i++) {
        let row = document.createElement("tr");
        row.classList.add("variant");
        let tableCell = document.createElement("td");
        row.appendChild(tableCell);

        tableCell.innerText = currentDataBase.tables[i].name;
        row.addEventListener("click", () => {
            selectedTableInfo(currentDataBase.id, currentDataBase.tables[i].id);
        });
        table.appendChild(row);
    }

}
function selectedTableInfo(databaseId, tableId) {

    let filteredDataBaseList = databasesList.filter((element) => {
        return element.id === databaseId;
    });
    let currentTable = filteredDataBaseList[0].tables.filter((element) => {
        return element.id === tableId;
    });

    let separator = document.createElement("div");
    separator.classList.add("link_separator");
    separator.innerText = '>';
    links.appendChild(separator);
    let link = document.createElement("div");
    link.classList.add("link");
    link.innerText = currentTable[0].name;
    links.appendChild(link);

    link.addEventListener('click', () => {
        updateLinks(tableName);
        selectedTableInfo(databaseId, tableId);
    });
    linksList.push(tableId);

    dataPart.innerHTML = '';


    let table = document.createElement("table");
    dataPart.appendChild(table);

    let headerRow = document.createElement("tr");
    headerRow.id = 'tableHeaders';
    for (let i = 0; i < currentTable[0].columns.length; i++) {
        let header1 = document.createElement("th");
        header1.innerText = currentTable[0].columns[i].name;
        headerRow.appendChild(header1);
    }
    table.appendChild(headerRow);

    for (let i = 0; i < currentTable[0].rows.length; i++) {
        let row = document.createElement("tr");
        row.classList.add("variant");
        for (let j = 0; j < currentTable[0].rows[i].length; j++) {
            let tableCell = document.createElement("td");
            row.appendChild(tableCell);

            tableCell.innerText = currentTable[0].rows[i][j];
        }
        row.addEventListener("click", () => {
            selectedTableInfo(currentDataBase.tables[i].name);
        });
        table.appendChild(row);
    }

}



