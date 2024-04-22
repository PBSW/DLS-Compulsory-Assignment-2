pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                sh 'docker compose build'
            }
        }  
        stage('Test') {
            steps {
                sh 'dotnet test Backend/PS-Tests'
                sh 'dotnet test Backend/MS-Tests'
            }
        }
        stage('Upload docker images') {
            steps {
                withCredentials([usernamePassword(credentialsId: 'Dockerhub', usernameVariable: 'DOCKER_USER', passwordVariable: 'DOCKER_PASS')]) 
                {
                    sh 'docker login -u $DOCKER_USER -p $DOCKER_PASS'
                    sh 'docker compose push'
                }
            }
        }
        stage('Deploy') {
            steps {
                echo 'serve application'
            }
        }
    }
}
