function getUsers() {
    // For now, consider the data is stored on a static `users.json` file
    return fetch('./users.json')
        // the JSON body is taken from the response
        .then(res => res.json())
        .then(res => {
        // The response has an `any` type, so we need to cast
        // it to the `User` type, and return it from the promise
        return res;
    });
}
function createUser(name, email) {
    return fetch('http://localhost:7071/api/person?name=' + name + '&email=' + email)
        // the JSON body is taken from the response
        .then(res => res.json())
        .then(res => {
        // The response has an `any` type, so we need to cast
        // it to the `User` type, and return it from the promise
        return res;
    });
}
//# sourceMappingURL=index.js.map