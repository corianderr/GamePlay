import useAxiosPrivate from "hooks/useAxiosPrivate";
import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";

const AddToCollectionForm = ({
  handleSubmit,
  availableCollections,
  gameId,
}) => {
  const axiosPrivate = useAxiosPrivate();
  const [errMsg, setErrMsg] = useState("");
  const [collectionId, setCollectionId] = useState(null);
  const { t } = useTranslation();

  useEffect(() => {
    setErrMsg("");
  }, [collectionId]);

  useEffect(() => {
    if (availableCollections.length > 0) {
      setCollectionId(availableCollections[0].id);
    }
  }, [availableCollections]);

  const chooseCollection = async (e) => {
    e.preventDefault();
    if (collectionId === null) {
      setErrMsg(t("collection.chooseAtLeastOneMes"));
      return;
    }

    const response = await axiosPrivate.post(
      `collection/addGame?id=${gameId}&collectionId=${collectionId}`,
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
      }
    );
    handleSubmit();
  };

  return (
    <>
      <p className={errMsg ? "errmsg" : "offscreen"} aria-live="assertive">
        {errMsg}
      </p>

      <form onSubmit={chooseCollection}>
        <div className="form-group">
          <select
            className="form-control"
            id="collectionSelect"
            name="collectionId"
            onChange={(e) => {
              setCollectionId(e.target.value);
            }}
            required
          >
            {availableCollections.map((collection, i) => (
              <option value={collection.id} key={i} selected={i == 0}>
                {collection.name}
              </option>
            ))}
          </select>
        </div>
        <button className="btn btn-warning">{t("game.addToCollection")}</button>
      </form>
    </>
  );
};

export default AddToCollectionForm;
