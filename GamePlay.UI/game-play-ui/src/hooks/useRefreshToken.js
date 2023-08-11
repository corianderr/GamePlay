import axios from '../api/axios';
import useAuth from './useAuth';

const useRefreshToken = () => {
    const { setAuth } = useAuth();

    const refresh = async () => {
        const response = await axios.get('user/refresh', {
            withCredentials: true
        });
        setAuth(prev => {
            return { ...prev, 
                accessToken: response.data.result.accessToken,
                roles: response.data.result.roles,
                id: response.data.result.id,
                username: response.data.result.username
            }
        });
        return response.data.result.accessToken;
    }
    return refresh;
};

export default useRefreshToken;