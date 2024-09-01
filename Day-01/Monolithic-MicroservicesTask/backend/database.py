from sqlalchemy import create_engine
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import sessionmaker

# SQLALCHEMY_DATABASE_URL = "sqlite:///./../database/db.sqlite"
# SQLALCHEMY_DATABASE_URL = "sqlite:///./database/db.sqlite"
SQLALCHEMY_DATABASE_URL = "postgresql://postgres:_admin_123@db:5432/test_db"

engine = create_engine(SQLALCHEMY_DATABASE_URL)
SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)

Base = declarative_base()
