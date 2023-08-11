import useAxiosPrivate from "hooks/useAxiosPrivate";
import { useEffect, useState } from "react";

const AddToCollectionForm = ({
  handleSubmit,
  availableCollections,
  gameId,
}) => {
  const axiosPrivate = useAxiosPrivate();
  const [errMsg, setErrMsg] = useState("");
  const [collectionId, setCollectionId] = useState(null);

  useEffect(() => {
    setErrMsg("");
  }, [collectionId]);

  const chooseCollection = async (e) => {
    e.preventDefault();
    if (collectionId === null) {
      setErrMsg("You should choose at least one collection.");
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
            <option selected="true" disabled="disabled">
              Choose Collection
            </option>
            {availableCollections.map((collection, i) => (
              <option value={collection.id} key={i}>
                {collection.name}
              </option>
            ))}
          </select>
        </div>
        <button className="btn btn-warning">Add to collection</button>
      </form>
    </>
  );
};

export default AddToCollectionForm;
