import axios from '../api/axios';
import useAuth from './useAuth';

const useRefreshToken = () => {
    const { setAuth } = useAuth();

    const refresh = async () => {
        const response = await axios.get('User/refresh', {
            withCredentials: true
        });
        setAuth(prev => {
            console.log(JSON.stringify(prev));
            console.log(response.data.result);
            return { ...prev, 
                accessToken: response.data.result.accessToken,
                roles: response.data.result.roles,
                id: response.data.result.id
            }
        });
        return response.data.result;
    }
    return refresh;
};

export default useRefreshToken;