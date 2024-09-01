from fastapi import FastAPI, Depends
from fastapi.middleware.cors import CORSMiddleware
from sqlalchemy.orm import Session
from . import models, database, schemas

database.Base.metadata.create_all(bind=database.engine)

app = FastAPI(root_path="/api")

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)


def get_db():
    db = database.SessionLocal()
    try:
        yield db
    finally:
        db.close()


@app.get("/data")
def read_data(db: Session = Depends(get_db)):
    items = db.query(models.User).all()
    return items


@app.post("/data", response_model=schemas.UserCreate)
def create_data(item: schemas.UserCreate, db: Session = Depends(get_db)):
    db_item = models.User(name=item.name)
    db.add(db_item)
    db.commit()
    db.refresh(db_item)
    return db_item
