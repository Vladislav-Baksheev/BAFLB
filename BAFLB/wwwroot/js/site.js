const uri = 'game/users';
let users = [];
const select = document.formUsers.users;
let round;
let lastPlayerShoot = -1;

function getUsers() {
  fetch(uri)
    .then(response => response.json())
    .then(data => _displayUsers(data))
    .catch(error => console.error('Unable to get items.', error));
}

function getRound() {
    fetch("game/round")
        .then(response => response.json())
        .then(dataR => _displayRound(dataR))
        .catch(error => console.error('Unable to get items.', error));
}

function addUser() {
  const addNameTextbox = document.getElementById('add-name');

  const item = {
      Name: addNameTextbox.value.trim()
  };

  fetch(uri + "/add", {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  })
    .then(response => response.json())
    .then(() => {
      getUsers();
      addNameTextbox.value = '';
    })
    .catch(error => console.error('Unable to add item.', error));
}

function deleteUser(id) {
    fetch(`${uri}/delete/${id}`, {
        method: 'DELETE'
  })
  .then(() => getUsers())
  .catch(error => console.error('Unable to delete item.', error));
}


function _displayUsers(data) {
    const tBody = document.getElementById('users-table');

    tBody.innerHTML = '';

  const button = document.createElement('button');
    while (select.options.length > 0) {
        select.remove(0);
    }

    data.forEach(user => {
        
        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Удалить';
        deleteButton.setAttribute('onclick', `deleteUser(${user.id})`);

        const newOption = new Option(user.name, user.name);

        select.options.add(newOption);

        let tr = tBody.insertRow();
        tr.classList.add('new-row');
        let td1 = tr.insertCell(0);
        td1.textContent = user.name;

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(user.currentShot + "/ 6");
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        if (user.isDead == true) {
            td3.textContent = "умер";

            var op = document.getElementById("users").getElementsByTagName("option");
            if (op[select.selectedIndex].value.toLowerCase() == user.name) {
                op[select.selectedIndex].disabled = true;
            }

        }
        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });
    select.selectedIndex = lastPlayerShoot;
    users = data;
}

function _displayRound(data) {
    const td = document.getElementById('round');
    td.textContent = data.name;
}

function startGame() {
    
    fetch(`game/start`, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    })
        .then(response => response.json())
        .then(json => {
            _displayRound(json.round);
            _displayUsers(json.users)
        })

}

function userShoot() {
    let user = users[select.selectedIndex];
    lastPlayerShoot = select.selectedIndex;

    fetch(`game/shoot`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user.id)
    })
        .then(response => response.json())
        .then(json => {
            user.currentShot = json.currentShot;
            user.isDead = json.isDead;

            if (json.isDead) {
                play();
            }

            _displayUsers(users)
        })
}

function play() {
    let user = users[select.selectedIndex];
    if (user.isDead == true) {
        var audio = new Audio('https://interactive-examples.mdn.mozilla.net/media/cc0-audio/t-rex-roar.mp3');
        audio.play();
    } 
}

function closeOrDisplayInput() {
    if (document.getElementById('editForm').style.display == 'none') {
        document.getElementById('editForm').style.display = 'block';
    }
    else {
        document.getElementById('editForm').style.display = 'none';
    }
    
}
