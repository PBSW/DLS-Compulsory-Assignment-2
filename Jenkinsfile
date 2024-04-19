pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                sh 'docker compose build'
            }
        }
        stage('Run') {
            steps {
                echo 'docker compose up -d'
            }
        }  
        stage('Test') {
            steps {
                echo 'docker compose test'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....'
            }
        }
    }
}
