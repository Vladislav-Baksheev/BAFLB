const uri = 'game/users';
let users = [];
const select = document.formUsers.users;
function getUsers() {
  fetch(uri)
    .then(response => response.json())
    .then(data => _displayUsers(data))
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

function deleteUser() {
    let user = users[select.selectedIndex];
    fetch(`${uri}/delete/${user.id}`, {
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
        deleteButton.innerText = 'Delete';
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
            play();
        }
    });

  users = data;
}

function startGame() {
    fetch(`game/start`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(users)
    })
        /*.then(response => response.json())*/
        .then(() => getUsers());
}

function userShoot() {
    let selectedOption = select.options[select.selectedIndex];
    let user = users[select.selectedIndex];


    fetch(`game/shoot`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    })
        /*.then(response => response.json())*/
        .then(() => getUsers());
}

function play() {
    var audio = new Audio('https://interactive-examples.mdn.mozilla.net/media/cc0-audio/t-rex-roar.mp3');
    audio.play();
}