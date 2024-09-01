from pydantic import BaseModel

class UserCreate(BaseModel):
    name: str

    class Config:
        from_attributes = True
