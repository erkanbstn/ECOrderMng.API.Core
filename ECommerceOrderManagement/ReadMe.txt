1. Yeni Sipariş Ekleme (POST)
POST /api/orders

Örnek Request
{
    "customerId": 1,
    "orderItems": [
        {
            "productId": 1,
            "quantity": 2,
            "unitPrice": 20.00
        },
        {
            "productId": 2,
            "quantity": 1,
            "unitPrice": 50.00
        }
    ],
    "totalAmount": 90.00,
    "status": "Yeni"
}

-------------------------------------------

2. Siparişleri Listeleme (GET)
GET /api/orders

Örnek Response
[
    {
        "id": 1,
        "customerId": 1,
        "orderDate": "2023-09-01T10:30:00",
        "totalAmount": 90.00,
        "status": "Yeni",
        "orderItems": [
            {
                "productId": 1,
                "quantity": 2,
                "unitPrice": 20.00,
                "totalPrice": 40.00
            },
            {
                "productId": 2,
                "quantity": 1,
                "unitPrice": 50.00,
                "totalPrice": 50.00
            }
        ]
    },
    {
        "id": 2,
        "customerId": 2,
        "orderDate": "2023-09-02T15:00:00",
        "totalAmount": 150.00,
        "status": "Onaylı",
        "orderItems": [
            {
                "productId": 3,
                "quantity": 3,
                "unitPrice": 30.00,
                "totalPrice": 90.00
            },
            {
                "productId": 1,
                "quantity": 2,
                "unitPrice": 20.00,
                "totalPrice": 40.00
            }
        ]
    }
]

-------------------------------------------

3. Sipariş Detayını Getirme (GET)
GET /api/orders/{id}

Örnek Response

{
    "id": 1,
    "customerId": 1,
    "orderDate": "2023-09-01T10:30:00",
    "totalAmount": 90.00,
    "status": "Yeni",
    "orderItems": [
        {
            "productId": 1,
            "quantity": 2,
            "unitPrice": 20.00,
            "totalPrice": 40.00
        },
        {
            "productId": 2,
            "quantity": 1,
            "unitPrice": 50.00,
            "totalPrice": 50.00
        }
    ]
}

-------------------------------------------

4. Sipariş Silme (DELETE)
DELETE /api/orders/{id}

-------------------------------------------