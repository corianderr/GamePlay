import useAxiosPrivate from "hooks/useAxiosPrivate";
import { useEffect, useState } from "react";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import UserList from "../../components/user/UserList/UserList";
import { useTranslation } from "react-i18next";

const Friends = () => {
    const [subscribers, setSubscribers] = useState([]);
    const axiosPrivate = useAxiosPrivate();
    const navigate = useNavigate();
    const location = useLocation();
    const { t } = useTranslation();

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
        <UserList header={t("user.friends")} users={subscribers} />
      </>
    );
}

export default Friends