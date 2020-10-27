# WPF Validierung

## Validierung des `EmployeeViewModel`

### Vorname prüfen

![Vorname prüfen](images/validate-firstname.png)

### Nachname prüfen

![Nachname prüfen](images/validate-lastname.png)

### Allgemeine Überprüfungen

![ViewModel prüfen](./images/validate-employeeviewmodel.png)

### Fehlermeldungen als Tooltip

![Fehlermeldungen als Tooltip](./images/style-errorastooltip.png)

### Ergebnis

![Ergebnis EmployeeViewModel](./images/validate-employeeviewmodel-screenshot.png)

## Validierung des `NewActivityViewModel`

### DB-Validierungen

#### Erweiterung des Kommandos zum Speichern:

![DB-Validierung](./images/validate-db.png)

#### Erweiterung der View (`NewActivityWindow.xaml`) -> DbError

![DbError binden](./images/bind-to-dberror.png)

#### Style für die Fehlerbeschreibung `ErrorCaptionStyle`

![DbError binden](./images/style-errorcaption.png)

#### Erweiterung  `UnitOfWork`

![UnitOfWork Erweiterung](./images/validate-unitofwork.png)

### Ergebnis

![Ergebnis NewActivityViewModel](./images/validate-newactivityviewmodel-screenshot.png)

