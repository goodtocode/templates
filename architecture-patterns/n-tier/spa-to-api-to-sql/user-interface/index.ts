interface Person {
    name: string
    email: string
}

function getUsers(url :string = './data.json'): Promise<Person[]> {
    // For now, consider the data is stored on a static `data.json` file
    return fetch(url)
            // the JSON body is taken from the response
            .then(res => res.json())
            .then(res => {
                    // The response has an `any` type, so we need to cast
                    // it to the `User` type, and return it from the promise
                    return res as Person[]
            })
}

function createUser(name :string, email :string) {

    return fetch('http://localhost:7071/api/person?name=' + name + '&email=' + email)
            // the JSON body is taken from the response
            .then(res => res.json())
            .then(res => {
                    // The response has an `any` type, so we need to cast
                    // it to the `User` type, and return it from the promise
                    return res as Person[]
            })
}