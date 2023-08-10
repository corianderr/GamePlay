import { useState, useEffect } from "react";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import { useNavigate, useLocation, Link, useParams } from "react-router-dom";
import GameList from "./GameList";

const CollectionDetails = () => {
  const [collection, setCollection] = useState({games: [], name: ""});
  const axiosPrivate = useAxiosPrivate();
  const navigate = useNavigate();
  const location = useLocation();

  const { collectionId } = useParams();

  useEffect(() => {
    let isMounted = true;
    const controller = new AbortController();

    const getGames = async () => {
      try {
        const response = await axiosPrivate.get(
          `/collection/getById/${collectionId}`,
          {
            signal: controller.signal,
          }
        );
        console.log(response.data);
        isMounted && setCollection(response.data.result);
      } catch (err) {
        if (err.name === "CanceledError") {
          return;
        }

        console.error(err);
        navigate("/login", { state: { from: location }, replace: true });
      }
    };
    getGames();

    return () => {
      isMounted = false;
      controller.abort();
    };
  }, []);

  return <GameList games={collection.games} header={collection.name}/>;
};

export default CollectionDetails;
