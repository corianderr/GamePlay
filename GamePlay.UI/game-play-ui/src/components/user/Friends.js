import useAxiosPrivate from "hooks/useAxiosPrivate";
import { useEffect, useState } from "react";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import UserList from "./UserList";
import useAuth from "hooks/useAuth";

const Friends = () => {
    const [subscribers, setSubscribers] = useState([]);
    const axiosPrivate = useAxiosPrivate();
    const navigate = useNavigate();
    const location = useLocation();
    const { auth } = useAuth();

    const { userId } = useParams();
  
    useEffect(() => {
      let isMounted = true;
      const controller = new AbortController();
  
      const getUsers = async () => {
        try {
          const response = await axiosPrivate.get(`/user/showRelations/${userId}&true`, {
            signal: controller.signal,
          });
          console.log(response.data);
          isMounted && setSubscribers(response.data.result);
        } catch (err) {
          if (err.name === "CanceledError") {
            return;
          }
  
          console.error(err);
          navigate("/login", { state: { from: location }, replace: true });
        }
      };
      getUsers();
  
      return () => {
        isMounted = false;
        controller.abort();
      };
    }, []);
  
    return (
      <>
        <UserList header={"Friends"} users={subscribers} />
      </>
    );
}

export default Friends