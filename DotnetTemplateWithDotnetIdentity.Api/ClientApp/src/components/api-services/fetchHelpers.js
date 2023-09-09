import authService from '../api-authorization/AuthorizeService';

export const getApiData = async (url) => {
    
    try{
        const token = await authService.getAccessToken();
        const headers = new Headers();

        if(!!token) {
            const bearer = `Bearer ${token}`;
            headers.append("Authorization", bearer);
        }

        let options = {
            method: 'GET',
            headers: headers,
        }
        var response = await fetch(url, options);
        var responseData = await response.json();
        return responseData;
    }
    catch (err) {
        console.error("API error : ", err);
    }
}

export const postData = async (url, data) => {
    let response = await sendData(url, data, "POST");
    return response;
}

export const putData = async (url, data) => {
    let reponse = await sendData(url, data, "PUT");
    return reponse;
}

const sendData = async (url, data, method) => {
    try {

        const token = await authService.getAccessToken();
        const headers = new Headers();

        if (!!token) {
            const bearer = `Bearer ${token}`;
            headers.append("Authorization", bearer);
        }
        if (!!data) {
            headers.append('Content-Type', 'application/json')
        }
        let options = {
            method: 'POST',
            headers: headers,
            body: data ? JSON.stringify(data) : null,
        }
        var response = await fetch(url, options);
        var responseData = await response.json();
        return responseData;
    }
    catch (err) {
        console.error("API error : ", err);
    }
}