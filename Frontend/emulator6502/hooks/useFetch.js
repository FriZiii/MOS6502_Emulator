import { useState, useEffect } from "react";
import axios from "axios";

const useFetchGet = (endpoint, data) => {
    const [data, setData] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);

    const fetchData = async () => {
        setIsLoading(true);
        try {
            await axios.post(`https://mos6502-api20231128225336.azurewebsites.net/${endpoint}`, {
                data: data,
            })
            .then((response) => {
                setData(response.data.result);
                setIsLoading(false);
            });
        } catch (error) {
            setError(error);
            alert(`Error: ${error}`)
        } finally {
            setIsLoading(false)
        }
    }

    useEffect(() => {
        fetchData();
    }, [])

    const refetch = () => {
        setIsLoading(true);
        fetchData();
    }

    return { data, isLoading, error, refetch }
}

export default useFetchGet