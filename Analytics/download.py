import firebase_admin
from firebase_admin import credentials
from firebase_admin import firestore
import csv
import datetime

# constants
SERVICE_ACCOUNT_KEY_PATH = '/Users/gaphg/Downloads/pencils-down-player-analytics-firebase-adminsdk-fbsvc-c14df221cd.json'
COLLECTION_NAME = 'game_events_2' # change to 'game_events' for real data

cred = credentials.Certificate(SERVICE_ACCOUNT_KEY_PATH)
app = firebase_admin.initialize_app(cred)
db = firestore.client()

game_events_collection = db.collection(COLLECTION_NAME)
docs = game_events_collection.stream()

def flatten_doc(doc):
    data = doc.to_dict()
    for key, value in data['gameState'].items():
        data[f'gameState_{key}'] = value
    del data['gameState']
    return data

filename = COLLECTION_NAME + "_" + datetime.datetime.now().isoformat() + ".csv"
with open(filename, "w", newline='') as csvfile:
    first_row = flatten_doc(next(docs))
    fieldnames = list(first_row.keys())
    writer = csv.DictWriter(csvfile, fieldnames=fieldnames)
    writer.writeheader()
    writer.writerow(first_row)
    for doc in docs:
        row = flatten_doc(doc)
        writer.writerow(row)