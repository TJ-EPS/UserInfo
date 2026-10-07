const apiBase = '/api/users';

async function fetchUsers() {
  const res = await fetch(apiBase);
  return res.json();
}

function createRow(user) {
  const tr = document.createElement('tr');
  tr.innerHTML = `
    <td>${user.id}</td>
    <td>${user.firstName || ''} ${user.lastName || ''}</td>
    <td>${user.email || ''}</td>
    <td>
      <button class="delete" data-id="${user.id}">Delete</button>
    </td>
  `;
  return tr;
}

async function refresh() {
  const users = await fetchUsers();
  const tbody = document.querySelector('#usersTable tbody');
  tbody.innerHTML = '';
  users.forEach(u => tbody.appendChild(createRow(u)));
}

async function createUser() {
  const firstName = document.getElementById('firstName').value;
  const lastName = document.getElementById('lastName').value;
  const email = document.getElementById('email').value;
  await fetch(apiBase, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ firstName, lastName, email })
  });
  document.getElementById('firstName').value = '';
  document.getElementById('lastName').value = '';
  document.getElementById('email').value = '';
  await refresh();
}

document.addEventListener('click', async (e) => {
  if (e.target && e.target.id === 'createBtn') {
    await createUser();
  }
  if (e.target && e.target.classList.contains('delete')) {
    const id = e.target.getAttribute('data-id');
    await fetch(`${apiBase}/${id}`, { method: 'DELETE' });
    await refresh();
  }
});

window.addEventListener('load', () => refresh());
