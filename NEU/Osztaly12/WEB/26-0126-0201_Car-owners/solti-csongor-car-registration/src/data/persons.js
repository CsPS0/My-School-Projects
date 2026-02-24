const persons = [
  { id: 1, first_name: "Alex", last_name: "Morgan", email_address: "alex.morgan@example.com" },
  { id: 2, first_name: "Sam", last_name: "Lee", email_address: "sam.lee@example.com" },
  { id: 3, first_name: "Pat", last_name: "Kim", email_address: "pat.kim@example.com" },
];

export function findPersonById(id) {
  return new Promise((resolve, reject) => {
    if (!id) {
      reject(new Error("person_id is required"));
      return;
    }

    const person = persons.find((p) => p.id === Number(id));

    if (!person) {
      reject(new Error(`Person not found: ${id}`));
      return;
    }

    resolve(person);
  });
}